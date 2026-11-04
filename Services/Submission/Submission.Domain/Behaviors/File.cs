using FileStorage.Contracts;

namespace Submission.Domain.ValueObjects;

public partial class File
{
    private File() { }

    internal static File Create(UploadResponse uploadResponse, Asset asset, AssetTypeDefination assetType)
    {
        var fileName = Path.GetFileName(uploadResponse.FileName);
        var extension = FileExtension.FromFileName(fileName, assetType);
        return new File
        {
            Name = FileName.FromAsset(asset, extension),
            Extension = extension,
            OriginalName = fileName,
            Size = uploadResponse.FileSize,
            FileserverId = uploadResponse.FileId,
        };
    }
}
