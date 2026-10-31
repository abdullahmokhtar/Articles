using Blocks.Core.FluentValidation;
using Microsoft.AspNetCore.Http;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    [Required]
    public AssetType AssetType { get; init; }

    [Required]
    public IFormFile File { get; init; } = null!;

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(e => e.File)
            .NotNullWithMessage(nameof(UploadManuscriptFileCommand.File));

        RuleFor(e => e.AssetType)
            .Must(IsAssetTypeAllowed).WithMessage(e => $"{e.AssetType} not allowed");
    }

    private bool IsAssetTypeAllowed(AssetType assetType)
        => AllowedAssetTypes.Contains(assetType);

    private IReadOnlyCollection<AssetType> AllowedAssetTypes => new HashSet<AssetType> { AssetType.Manuscript };
}
