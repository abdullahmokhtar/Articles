using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Submission.Application.Features.CreateArticle;

internal class CreateAtricleCommandHandler(Repository<Journal> journalRepository) : IRequestHandler<CreateAtricleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateAtricleCommand command, CancellationToken cancellationToken)
    {
        var journal = await journalRepository.FindByIdOrThrowExceptionAsync(command.JournalId, cancellationToken);

        var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);

        await AssignCurrentUserAsAuthor(article, command);

        await journalRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }

    private async Task AssignCurrentUserAsAuthor(Article article, CreateAtricleCommand command)
    {
        var author = await journalRepository.Context.Authors.SingleOrDefaultAsync(e => e.UserId == command.CreatedBy);
        if (author is not null)
            article.AssignAuthor(author, [ContributionArea.OriginalDraft], isCorrespondingAuthor: true);
    }
}
