using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security;
using VidiSnizenja.Application.Features.Users.Commands.DeleteUser;
using VidiSnizenja.Application.Shared;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Users
{
    public static class DeleteUserEndpoint
    {
        public static RouteGroupBuilder MapDeleteUser(this RouteGroupBuilder group)
        {
            group.MapDelete("delete/{id}", DeleteUser)
                .RequireAuthorization(Permission.Customer, Permission.FullAccess)
                .Produces<ApiSuccessResponse<DeleteUserCommandResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<Ok<ApiSuccessResponse<DeleteUserCommandResponse>>> DeleteUser([FromBody] DeleteUserCommand command, ISender mediator)
        {
            var result = await mediator.Send(command);

            return TypedResults.Ok(new ApiSuccessResponse<DeleteUserCommandResponse>(result.Data));
        }
    }
}
