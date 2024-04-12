using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Domain.Entities;
using Infrastructure.Interfaces;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace remove_image_for_deleted_cavaleiro;

public class Function
{
    private const string BUCKET_NAME = "cavaleiros";
    private readonly IS3Service _s3Service;
    private readonly DynamoDBContext _dynamodbContext;

    public Function(IS3Service s3Service)
    {
        _s3Service = s3Service;
        _dynamodbContext = new DynamoDBContext(new AmazonDynamoDBClient());
    }

    public async void FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
    {
        foreach (var record in dynamoEvent.Records)
        {
            context.Logger.LogInformation($"Event ID: {record.EventID}");
            context.Logger.LogInformation($"Event Name: {record.EventName}");

            var dictionaryToAttributeValue = ConvertDictionaryToDictionary(record.Dynamodb.OldImage);
            var cavaleiro = ConvertDocumentToObject(dictionaryToAttributeValue);

            var response = await _s3Service.RemoveItem(BUCKET_NAME, cavaleiro.ReferenciaImagem);

            if (response) context.Logger.LogInformation("Image was deleted successful!");
            else context.Logger.LogError("Something it's wrong! It was not possible deleted the image.");
        }

        context.Logger.LogInformation("Stream processing complete.");
    }

    private Cavaleiro ConvertDocumentToObject(Dictionary<string, AttributeValue> image)
    {
        Document document = Document.FromAttributeMap(image);
        return _dynamodbContext.FromDocument<Cavaleiro>(document);
    }

    private Dictionary<string, AttributeValue> ConvertDictionaryToDictionary(Dictionary<string, DynamoDBEvent.AttributeValue> attributeValue)
    {
        Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>{};
        foreach (var item in attributeValue)
        {
            attributes.TryAdd(item.Key, MappingDynamoDBEventAttributeValueToDynamoDbV2AttributeValue(item.Value));
        }

        return attributes;
    }

    private AttributeValue MappingDynamoDBEventAttributeValueToDynamoDbV2AttributeValue(DynamoDBEvent.AttributeValue attributeValue)
    {
        return new AttributeValue
        {
            B = attributeValue.B, 
            BOOL = attributeValue.BOOL.Value,
            BS = attributeValue.BS,
            S = attributeValue.S,
            SS = attributeValue.SS,
            N = attributeValue.N,
            NULL = attributeValue.NULL.Value,
            NS = attributeValue.NS,
        };
    }
}