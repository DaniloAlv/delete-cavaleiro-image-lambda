using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using static Amazon.Lambda.DynamoDBEvents.DynamoDBEvent;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace remove_image_for_deleted_cavaleiro;

public class Function
{
    public void FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
    {
        context.Logger.LogInformation($"Beginning to process {dynamoEvent.Records.Count} records...");
        
        foreach (var record in dynamoEvent.Records)
        {
            context.Logger.LogInformation($"Event ID: {record.EventID}");
            context.Logger.LogInformation($"Event Name: {record.EventName}");
            
            record.Dynamodb.OldImage.TryGetValue("Message", out AttributeValue attributeValueOld);
            context.Logger.LogInformation($"Old Image: {attributeValueOld}");

            record.Dynamodb.NewImage.TryGetValue("Message", out AttributeValue attributeValueNew);
            context.Logger.LogInformation($"New Image: {attributeValueNew}");
        }

        context.Logger.LogInformation("Stream processing complete.");
    }
}