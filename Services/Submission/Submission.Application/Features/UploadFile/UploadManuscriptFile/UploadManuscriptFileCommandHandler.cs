using Blocks.EntityFramework;

namespace Submission.Application.Features.UploadFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository, Repository<AssetTypeDefination> assetTyperepository) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(request.ArticleId, cancellationToken);

        var assetType = await assetTyperepository.FindByIdAsync((int)request.AssetType, cancellationToken);

        Asset asset = null;

        if (!assetType.AllowsMultipleFiles)
            asset = article.Assets.SingleOrDefault(e => e.Type == assetType.Id);

        if (asset is null)
            asset = article.CreateAsset(assetType);

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(asset.Id);
    }
}
