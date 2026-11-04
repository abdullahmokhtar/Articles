using Blocks.EntityFramework;
using FileStorage.Contracts;

namespace Submission.Application.Features.UploadFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository, AssetTypeDefinationRepository assetTyperepository, IFileService fileService) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
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

        var filePath = asset.GenerateStorageFilePath(request.File.FileName);

        var uploadResponse = await fileService.UploadFileAsync(filePath, request.File, overwrite: true, tags: new Dictionary<string, string>
        {
            { "entity", nameof(Asset) },
            { "entityId", asset.Id.ToString() }
        }, cancellationToken);

        try
        {
            asset.CreateFile(uploadResponse, assetType);

            await articleRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await fileService.TryDeleteFileAsync(uploadResponse.FilePath, cancellationToken);
            throw;
        }

        return new IdResponse(asset.Id);
    }
}
