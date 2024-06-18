using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Articles;

public static class EditArticleEndpoint
{
    public static RouteGroupBuilder MapEditArticle(this RouteGroupBuilder builder)
    {
        builder.MapPost("/article/{id}", EditArticle)
            .RequireAuthorization()
            .Produces<ApiSuccessResponse<EditArticleCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return builder;
    }

    private static async Task<IResult> EditArticle([FromBody] EditArticleRequest request, ISender sender)
    {
        var result = await sender.Send(new EditArticleCommand(
            request.ArticleId,
            request.Name,
            request.ArticleTypeId,
            request.Price,
            request.Description,
            request.Discount,
            request.Status));

        return Results.Ok(new ApiSuccessResponse<EditArticleCommandResponse>(result.Data));
    }
}
