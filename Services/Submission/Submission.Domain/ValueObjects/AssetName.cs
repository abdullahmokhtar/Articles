using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class AssetName(string value) : StringValueObject(value)
{
    public static AssetName FromAssetType(AssetTypeDefination assetType)
        => new AssetName(assetType.Name.ToString());
}
