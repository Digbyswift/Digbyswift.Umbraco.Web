namespace Digbyswift.Umbraco.Web.Providers;

public interface IFileSystemProvider
{
    void Save(Stream stream, string filePath, string contentType, bool disposeOfStream = true);
    Task SaveAsync(Stream stream, string filePath, string contentType, bool disposeOfStream = true);

    bool Exists(string filePath);
    Task<bool> ExistsAsync(string filePath);

    bool Delete(string filePath);
    Task<bool> DeleteAsync(string filePath);

    Stream GetAsStream(string filePath, Stream stream);
    Task<Stream> GetAsStreamAsync(string filePath, Stream? stream = null);
}
