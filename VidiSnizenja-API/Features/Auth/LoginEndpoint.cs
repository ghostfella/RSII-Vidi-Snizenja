using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace VidiSnizenja_API.Features.Auth;

public static class LoginEndpoint
{
    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("/login", Login)
         .Produces<ApiSuccessResponse<LoginCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
         .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
         .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<IResult> Login([FromBody] LoginCommand login, IMediator mediator)
    {
        var result = await mediator.Send(login);

        return Results.Ok(new ApiSuccessResponse<LoginCommandResponse>(result.Data));
    }
}
