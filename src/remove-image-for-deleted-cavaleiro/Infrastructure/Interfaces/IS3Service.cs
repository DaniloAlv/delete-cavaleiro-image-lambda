namespace Infrastructure.Interfaces;

public interface IS3Service
{
    Task<bool> RemoveItem(string bucketName, string keyObject);
}