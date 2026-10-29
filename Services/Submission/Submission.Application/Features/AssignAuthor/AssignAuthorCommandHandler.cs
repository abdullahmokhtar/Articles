using Blocks.EntityFramework;

namespace Submission.Application.Features.AssignAuthor;

internal class AssignAuthorCommandHandler(ArticleRepository articleRepository) : IRequestHandler<AssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AssignAuthorCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(request.ArticleId);
        var author = await articleRepository.Context.Authors.FindByIdOrThrowExceptionAsync(request.AuthorId);

        article.AssignAuthor(author, request.ContributionAreas, request.IsCorrespondingAuthor);

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}
