using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Users
{
    public static class UpdateUserEndpoint
    {
        public static RouteGroupBuilder MapUpdateUser(this RouteGroupBuilder group)
        {
            group.MapPut("update/{id}", UpdateUser)
                .RequireAuthorization()
                .Produces<ApiSuccessResponse<UpdateUserCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<Ok<ApiSuccessResponse<UpdateUserCommandResponse>>> UpdateUser([FromRoute] string id, [FromBody] UpdateUserCommand request, IMediator mediator)
        {
            var result = await mediator.Send(request);

            return TypedResults.Ok(new ApiSuccessResponse<UpdateUserCommandResponse>(result.Data));
        }
    }
}
