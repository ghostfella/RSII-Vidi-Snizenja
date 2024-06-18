using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace VidiSnizenja_API.Features.Auth
{
    public static class RefreshTokenEndpoint
    {
        public static RouteGroupBuilder MapRefreshToken(this RouteGroupBuilder group)
        {
            group.MapPost("/refresh-token", RefreshToken)
           .Produces<ApiSuccessResponse<LoginCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<IResult> RefreshToken([FromBody] RefreshTokenCommand refreshToken, IMediator mediator)
        {
            var result = await mediator.Send(refreshToken);

            return Results.Ok(new ApiSuccessResponse<LoginCommandResponse>(result.Data));
        }
    }
}
