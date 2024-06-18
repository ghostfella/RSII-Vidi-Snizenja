using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace VidiSnizenja_API.Features.Articles;
    public static class CreateArticleEndpoint
    {
        public static RouteGroupBuilder MapCreateArticle(this RouteGroupBuilder builder)
        {
            builder.MapPost("/create-article", CreateArticle)
                   .RequireAuthorization()
                   .Produces<ApiSuccessResponse<CreateArticleCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
                   .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                   .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

            return builder;
        }

        public static async Task<IResult> CreateArticle([FromBody] CreateArticleRequest request, IMediator mediator)
        {
            var result = await mediator.Send(new CreateArticleCommand(
                request.Name,
                request.ArticleTypeId,
                request.Price,
                request.Description,
                request.Discount,
                request.Status));

            return Results.Ok(new ApiSuccessResponse<CreateArticleCommandResponse>(result.Data));
        }
    }
