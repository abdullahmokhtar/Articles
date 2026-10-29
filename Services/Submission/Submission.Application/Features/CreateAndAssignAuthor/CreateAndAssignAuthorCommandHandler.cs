using Blocks.EntityFramework;

namespace Submission.Application.Features.CreateAndAssignAuthor;

internal class CreateAndAssignAuthorCommandHandler(ArticleRepository articleRepository) : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(request.ArticleId, cancellationToken);

        Author? author = null;
        if (request.UserId is null)
        {
            author = Author.Create(request.Email!, request.FirstName!, request.LastName!, request.Title!, request.Affiliation!);
        }
        else
        {

        }

        article.AssignAuthor(author, request.ContributionAreas, request.IsCorrespondingAuthor);

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}
