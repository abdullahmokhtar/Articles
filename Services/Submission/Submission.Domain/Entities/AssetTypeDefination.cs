namespace Submission.Domain.Entities;

public class AssetTypeDefination : EnumEntity<AssetType>
{
    public required byte MaxFileSizeInMB { get; init; }

    public int MaxFileSizeInBytes => MaxFileSizeInMB * 1024 * 1024;

    public required string DefaultFileExtension { get; init; } = default!;

    public required FileExtensions AllowedFileExtensions { get; init; }
    public int MaxAssetCount { get; init; }

    public bool AllowsMultipleFiles => MaxAssetCount > 1;
}
