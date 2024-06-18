namespace VidiSnizenja_API.Features.Auth;

public static class AuthEndpoints
{
    private const string auth = "auth";
    private const string auth_path = $"api/{auth}";
    private const string auth_tag_name = "Authorization endpoints";
    
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(auth_path)
            .AllowAnonymous()
            .WithTags(auth_tag_name)
            .WithOpenApi();

        group.MapLogin();
        group.MapRefreshToken();

        return routes;
    }

}
