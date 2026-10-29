using Blocks.Core.FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateArticle;

public record CreateAtricleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType) : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateAtricleCommandValidator : AbstractValidator<CreateAtricleCommand>
{
    public CreateAtricleCommandValidator()
    {
        RuleFor(x => x.Title).NotEmptyWithMessage(nameof(CreateAtricleCommand.Title));
        RuleFor(x => x.Scope).NotEmptyWithMessage(nameof(CreateAtricleCommand.Scope));
        RuleFor(x => x.JournalId).GreaterThan(0).WithMessageForInvalidId(nameof(CreateAtricleCommand.JournalId));
    }
}

