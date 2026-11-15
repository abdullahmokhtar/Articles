using Articles.Abstractions.Enums;
using Auth.Domain.Users;
using Auth.Domain.Users.Events;
using Blocks.Exceptions;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Features.CreateUser;

[Authorize(Roles = Role.USERADMIN)]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> userManager) : Endpoint<CreateUserCommand, CreateUserResponse>
{
    public override async Task HandleAsync(CreateUserCommand req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);
        if (user is not null)
            throw new BadRequestException($"User with email {req.Email} already exists.");

        user = Domain.Users.User.Create(req);

        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new BadRequestException($"Unable to create user: {errorMessage}");
        }

        var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

        await PublishAsync(new UserCreated(user, resetPasswordToken));

        await Send.OkAsync(new CreateUserResponse(user.Email, user.Id, resetPasswordToken));
    }
}
