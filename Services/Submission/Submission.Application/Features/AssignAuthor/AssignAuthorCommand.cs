using Blocks.Core.FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.AssignAuthor;

public record AssignAuthorCommand(int AuthorId, bool IsCorrespondingAuthor, HashSet<ContributionArea> ContributionAreas) : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

public class AssignAuthorCommandValidator : AbstractValidator<AssignAuthorCommand>
{
    public AssignAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId).GreaterThan(0).WithMessageForInvalidId(nameof(AssignAuthorCommand.AuthorId));
        RuleFor(x => x.ContributionAreas).NotEmptyWithMessage(nameof(AssignAuthorCommand.ContributionAreas));
    }
}
