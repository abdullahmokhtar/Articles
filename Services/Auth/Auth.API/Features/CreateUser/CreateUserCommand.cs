using Articles.Abstractions.Enums;
using Auth.Domain.Users;
using Auth.Domain.Users.Enums;

namespace Auth.API.Features.CreateUser;

public class CreateUserCommand : ICreateUserInfo
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public required Gender Gender { get; init; }

    public required Honorific? Honorific { get; init; }

    public required string? PhoneNumber { get; init; }
    public required string? PictureUrl { get; init; }
    public required string? CompanyName { get; init; }
    public required string? Position { get; init; }
    public required string? Affiliation { get; init; }
    public required IReadOnlyList<IUserRole> UserRoles { get; init; } = [];
}



public record UserRoleDto(
    UserRoleType Type,
    DateTime? StartDate,
    DateTime? ExpiringDate) : IUserRole;

public record CreateUserResponse(
    string Email,
    int UserId,
    string Token);