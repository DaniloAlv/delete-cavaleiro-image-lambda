using Amazon.S3;
using Amazon.S3.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _amazonS3;

    public S3Service(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }

    public async Task RemoveItem(string bucketName, string keyObject)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = keyObject
        };

        await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
    }
}