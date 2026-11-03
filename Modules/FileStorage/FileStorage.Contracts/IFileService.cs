using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public interface IFileService
{
    Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null, CancellationToken cancellationToken = default);
    Task<(Stream FileStram, string ContentType)> DownloadFileAsync(string fileId, CancellationToken cancellationToken = default);
    Task<bool> TryDeleteFileAsync(string fileId, CancellationToken cancellationToken = default);
}
