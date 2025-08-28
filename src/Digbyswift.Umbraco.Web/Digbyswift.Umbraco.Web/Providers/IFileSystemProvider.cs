namespace Digbyswift.Umbraco.Web.Providers;

[Obsolete("This class is no longer used and will be removed in future versions. Instead use Umbraco.StorageProviders.AzureBlob.IO.IAzureBlobFileSystem")]
public interface IFileSystemProvider
{
    void Save(Stream stream, string filePath, string contentType, bool disposeOfStream = true);
    Task SaveAsync(Stream stream, string filePath, string contentType, bool disposeOfStream = true);

    bool Exists(string filePath);
    Task<bool> ExistsAsync(string filePath);

    bool Delete(string filePath);
    Task<bool> DeleteAsync(string filePath);

    [Obsolete("Use Umbraco.StorageProviders.AzureBlob.IO.IAzureBlobFileSystem.OpenFile() instead.")]
    Stream GetAsStream(string filePath, Stream stream);
    Task<Stream> GetAsStreamAsync(string filePath, Stream? stream = null);
}
