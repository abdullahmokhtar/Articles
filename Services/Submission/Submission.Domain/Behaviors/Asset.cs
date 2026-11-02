namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset()
    {

    }

    internal static Asset Create(Article article, AssetTypeDefination type)
    {
        return new Asset
        {
            ArticleId = article.Id,
            Article = article,
            Name = AssetName.FromAssetType(type),
            Type = type.Name,
        };
    }
}
