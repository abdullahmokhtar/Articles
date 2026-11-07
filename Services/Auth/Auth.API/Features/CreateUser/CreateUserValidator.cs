using FastEndpoints;
using FluentValidation;

namespace Auth.API.Features.CreateUser;

public class CreateUserValidator : Validator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(e => e.UserRoles).NotEmpty()
            .Must((c, roles) => AreUserRoleDatesValid(roles)).WithMessage("Invalid Role");
    }

    public static bool AreUserRoleDatesValid(IReadOnlyList<UserRoleDto> roles)
    {
        return roles.All(role =>
        (!role.StartDate.HasValue || role.StartDate.Value.Date >= DateTime.UtcNow.Date) &&
        (!role.ExpiringDate.HasValue || (role.StartDate ?? DateTime.UtcNow).Date < role.ExpiringDate.Value.Date));
    }
}
