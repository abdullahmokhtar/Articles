using Blocks.EntityFramework;

namespace Submission.Application.Features.UploadFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(request.ArticleId, cancellationToken);

        return new IdResponse(0);
    }
}
