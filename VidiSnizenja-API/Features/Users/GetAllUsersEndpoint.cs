using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Users
{
    public static class GetAllUsersEndpoint
    {
        public static RouteGroupBuilder MapGetAllUsers(this RouteGroupBuilder group)
        {
            group.MapGet("get-all", GetAllUsers)
                .AllowAnonymous()
                .Produces<ApiSuccessResponse<List<GetAllUsersResponse>>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<Ok<ApiSuccessResponse<List<GetAllUsersResponse>>>> GetAllUsers([AsParameters] GetAllUsersRequest request, IMediator mediator)
        {
            var result = await mediator.Send(request);

            return TypedResults.Ok(new ApiSuccessResponse<List<GetAllUsersResponse>>(result.Data));
        }
    }
}
