using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mime;
using VidiSnizenja_API.Shared;

namespace VidiSnizenja_API.Features.Users
{
    public static class GetUserByIdEndpoint
    {
        public static RouteGroupBuilder MapGetUserById(this RouteGroupBuilder group)
        {
            group.MapGet("get-by-id", GetUserById)
                .RequireAuthorization()
                .Produces<ApiSuccessResponse<GetUserByIdResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
                .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

            return group;
        }

        public static async Task<Ok<ApiSuccessResponse<GetUserByIdResponse>>> GetUserById([AsParameters] GetUserByIdRequest request, IMediator mediator)
        {
            var result = await mediator.Send(request);

            return TypedResults.Ok(new ApiSuccessResponse<GetUserByIdResponse>(result.Data));
        }
    }
}
