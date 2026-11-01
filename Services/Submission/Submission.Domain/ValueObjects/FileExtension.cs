using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class FileExtension(string value) : StringValueObject(value)
{
    public static FileExtension FromFileName(string fileName, AssetTypeDefination assetType)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1);
        Guard.ThrowIfNullOrWhitespace(extension);
        Guard.ThrowIfNotEqual(
            assetType.AllowedFileExtensions.IsValidExtension(extension), true);

        return new FileExtension(extension);
    }
}
