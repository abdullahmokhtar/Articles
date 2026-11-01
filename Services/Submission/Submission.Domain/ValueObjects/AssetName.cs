using Articles.Abstractions.Enums;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class AssetName(string value) : StringValueObject(value)
{
    public static AssetName FromAssetType(AssetType assetType)
        => new AssetName(assetType.ToString());
}
