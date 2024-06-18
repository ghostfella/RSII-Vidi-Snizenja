using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Users
{
    public static class CreateUserEndpoint
    {
        public static RouteGroupBuilder MapCreateUser(this RouteGroupBuilder group)
        {
            group.MapPost("create", CreateUser)
                .AllowAnonymous()
                   .Produces<ApiSuccessResponse<CreateUserCommandResponse>>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
                   .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                   .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<Created<ApiSuccessResponse<CreateUserCommandResponse>>> CreateUser([FromBody] CreateUserCommand command, ISender mediator)
        {
            var result = await mediator.Send(command);

            return TypedResults.Created(new Uri(""), new ApiSuccessResponse<CreateUserCommandResponse>(result.Data));
        }
    }
}
