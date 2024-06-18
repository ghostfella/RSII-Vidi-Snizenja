using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Articles;

public static class DeleteArticleEndpoint
{
    public static RouteGroupBuilder MapDeleteArticle(this RouteGroupBuilder builder)
    {
        builder.MapDelete("/article/{id}", DeleteArticle)
            .RequireAuthorization()
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return builder;
    }

    private static async Task<IResult> DeleteArticle([FromBody] DeleteArticleRequest request, ISender sender)
    {
        var result = await sender.Send(new DeleteArticleCommand(request.Id));

        return TypedResults.Ok(new ApiSuccessResponse<DeleteArticleCommandResponse>(result.Data));
    }
}
