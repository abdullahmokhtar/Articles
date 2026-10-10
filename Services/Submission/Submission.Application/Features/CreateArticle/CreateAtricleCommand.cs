using Articles.Abstractions;
using Articles.Abstractions.Enums;
using FluentValidation;
using MediatR;

namespace Submission.Application.Features.CreateArticle;

public record CreateAtricleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType) : IRequest<IdResponse> { }

public class CreateAtricleCommandValidator : AbstractValidator<CreateAtricleCommand>
{
    public CreateAtricleCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.Scope).NotEmpty().WithMessage("Scope cannot be empty.");
        RuleFor(x => x.JournalId).GreaterThan(0).WithMessage("Invalid Journal Id.");
    }
}

