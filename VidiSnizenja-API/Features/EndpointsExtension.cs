using VidiSnizenja_API.Features.Auth;
using VidiSnizenja_API.Features.Users;

namespace VidiSnizenja_API.Features
{
    public static class EndpointsExtension
    {
        public static IEndpointRouteBuilder UseAppEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGroup("vs")
                .MapAuthEndpoints()
                .MapUserEndpoints();

            return routes;
        }
    }
}
