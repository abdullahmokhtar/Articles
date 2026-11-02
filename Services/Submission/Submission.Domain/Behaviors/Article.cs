using Blocks.Domain;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

        if (Actors.Any(a => a.PersonId == author.Id && a.Role == role))
            throw new DomainException($"Author {author.EmailAddress} is already assigned to this article.");

        var articleAuthor = new ArticleAuthor
        {
            ContributionAreas = contributionAreas,
            Person = author,
            Role = role,
        };
        Actors.Add(articleAuthor);
    }

    public Asset CreateAsset(AssetTypeDefination type)
    {
        var assetCount = _assets.Count(e => e.Type == type.Id);

        if (type.MaxAssetCount > assetCount - 1)
            throw new DomainException($"Asset type {type.Name} has reached its maximum count of {type.MaxAssetCount} for this article.");

        var asset = Asset.Create(this, type);
        _assets.Add(asset);

        return asset;
    }
}
