using Articles.Abstractions.Enums;

namespace Auth.API.Features.CreateUser;

public class CreateUserCommand
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public required int Gender { get; init; }

    public required string? Honorific { get; init; }

    public required string? PhoneNumber { get; init; }
    public required string? PictureUrl { get; init; }
    public required string? CompanyName { get; init; }
    public required string? Position { get; init; }
    public required string? Affiliation { get; init; }
    public required IReadOnlyList<UserRoleDto> UserRoles { get; init; } = [];
}

public record UserRoleDto(
    UserRoleType Type,
    DateTime? StartDate,
    DateTime? ExpiringDate);

public record CreateUserResponse(
    string Email,
    int UserId,
    string Token);