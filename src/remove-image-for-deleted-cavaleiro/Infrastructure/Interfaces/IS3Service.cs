namespace Infrastructure.Interfaces;

public interface IS3Service
{
    Task RemoveItem(string bucketName, string keyObject);
}