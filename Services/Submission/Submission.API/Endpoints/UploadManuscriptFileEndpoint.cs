using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.UploadFile;

namespace Submission.API.Endpoints;

public static class UploadManuscriptFileEndpoint
{
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles/{articleId:int}/assets/manuscript:upload",
            async ([FromRoute] int articleId, [FromForm] UploadManuscriptFileCommand command, ISender sender) =>
        {
            var response = await sender.Send(command with { ArticleId = articleId });
            return Results.Created($"api/articles/{command.ArticleId}/assets/{response.Id}:download", response);
        }).RequireRoleAuthorization(Role.CORAUT)
       .WithName("UploadManuscript")
       .WithTags("Assets")
       .Produces(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status401Unauthorized)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .ProducesValidationProblem(StatusCodes.Status400BadRequest)
       .DisableAntiforgery();
    }
}
