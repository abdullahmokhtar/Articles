using FluentValidation;

namespace Blocks.Core.FluentValidation;

public static class Extensions
{
    public static IRuleBuilderOptions<T, TProperty> WithMessageForInvalidId<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string propertyName)
        => rule.WithMessage(c => ValidationMessages.InvalidId.FormatWith(propertyName));

    public static IRuleBuilder<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> rule, string propertyName)
    => rule.NotEmpty().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));

    public static IRuleBuilder<T, string?> MaximumLengthWithMessage<T>(this IRuleBuilder<T, string?> rule, int maxLength, string propertyName)
        => rule.MaximumLength(maxLength).WithMessage(c => ValidationMessages.MaxLengthExceeded.FormatWith(propertyName, maxLength));
}
