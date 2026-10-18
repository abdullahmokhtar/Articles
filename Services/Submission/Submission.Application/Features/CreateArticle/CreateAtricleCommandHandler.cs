using Articles.Abstractions;
using Blocks.EntityFramework;
using MediatR;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;

namespace Submission.Application.Features.CreateArticle;

internal class CreateAtricleCommandHandler(Repository<Journal> journalRepository) : IRequestHandler<CreateAtricleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateAtricleCommand command, CancellationToken cancellationToken)
    {
        var journal = await journalRepository.FindByIdOrThrowExceptionAsync(command.JournalId, cancellationToken);

        var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);
        await journalRepository.SaveChangesAsync(cancellationToken);
        return new IdResponse(article.Id);
    }
}
