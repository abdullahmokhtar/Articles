using Blocks.EntityFramework;

namespace Submission.Application.Features.UploadFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository, AssetTypeDefinationRepository assetTyperepository) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(request.ArticleId, cancellationToken);

        var assetType = assetTyperepository.GetById(request.AssetType);

        Asset asset = null;

        if (!assetType.AllowsMultipleFiles)
            asset = article.Assets.SingleOrDefault(e => e.Type == assetType.Id);

        if (asset is null)
            asset = article.CreateAsset(assetType);

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(asset.Id);
    }
}
