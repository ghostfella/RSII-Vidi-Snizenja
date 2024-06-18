using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Articles;

public static class GetAllArticlesEndpoint
{
    public static RouteGroupBuilder MapGetAllArticles(this RouteGroupBuilder builder)
    {
        builder.MapPost("/get-all-article", GetAllArticles)
            .RequireAuthorization()
            .Produces<ApiSuccessResponse<IEnumerable<GetAllArticlesQueryResponse>>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return builder;
    }

    public static async Task<IResult> GetAllArticles([FromBody] GetAllArticlesRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new GetAllArticlesQuery(
            request.Name,
            request.MinPrice,
            request.MaxPrice,
            request.ArticleType,
            request.Status));

        return Results.Ok(new ApiSuccessResponse<IEnumerable<GetAllArticlesQueryResponse>>(result.Data));
    }
}
