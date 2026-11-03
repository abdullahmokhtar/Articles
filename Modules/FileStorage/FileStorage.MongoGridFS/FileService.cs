using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public class FileService : IFileService
{
    private readonly GridFSBucket _bucket;
    private readonly MongoGridFSFileStorageOptions _options;

    private const string FilePathMetadataKey = "filePath";
    private const string ContentTypeMetadataKey = "contentType";
    private const string DefaultContentType = "application/octet-stream";

    public FileService(GridFSBucket bucket, IOptions<MongoGridFSFileStorageOptions> options)
        => (_bucket, _options) = (bucket, options.Value);

    public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null, CancellationToken cancellationToken = default)
    {
        if (file?.Length > _options.FileSizeLimitInBytes)
            throw new InvalidOperationException($"File size exceeds the maximum allowed limit({_options.FileSizeLimitInMB})MB.");

        var metadata = new BsonDocument(tags ?? new Dictionary<string, string>()
        {
            {FilePathMetadataKey, filePath },
            { ContentTypeMetadataKey, file.ContentType }
        });

        var uploadOptions = new GridFSUploadOptions
        {
            Metadata = metadata,
            ChunkSizeBytes = _options.ChunkSizeBytes
        };

        ObjectId fileId;
        using var stream = file.OpenReadStream();
        fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions, cancellationToken);

        return new UploadResponse(filePath, file.FileName, file.Length, fileId.ToString());
    }

    public async Task<(Stream FileStram, string ContentType)> DownloadFileAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (!ObjectId.TryParse(fileId, out var objectId))
            throw new ArgumentException("Invalid file ID.", nameof(fileId));

        var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync(cancellationToken)
            ?? throw new FileNotFoundException($"File with ID '{fileId}' not found.");

        var stream = await _bucket.OpenDownloadStreamAsync(objectId, cancellationToken: cancellationToken);
        var contentType = fileInfo.Metadata?.GetValue(ContentTypeMetadataKey, DefaultContentType)?.AsString ?? DefaultContentType;

        return (stream, contentType);
    }

    public async Task<bool> TryDeleteFileAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (!ObjectId.TryParse(fileId, out var objectId))
            return false;

        try
        {
            await _bucket.DeleteAsync(objectId, cancellationToken);
            return true;
        }
        catch (GridFSFileNotFoundException)
        {
            return false;
        }
    }

}
