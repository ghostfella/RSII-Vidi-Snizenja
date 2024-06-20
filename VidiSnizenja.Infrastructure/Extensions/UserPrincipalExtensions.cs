using System.Security.Claims;
using VidiSnizenja.Infrastructure.Identity;

namespace VidiSnizenja.Infrastructure.Extensions;

public static class UserPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Name);
    public static string GetEmail(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Email);
    public static string GetId(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.NameIdentifier);
    public static string[] GetRoles(this ClaimsPrincipal principal) => principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

    public static CurrentUser GetUserFromPrincipal(this ClaimsPrincipal principal)
    {
        return new CurrentUser(principal.GetUsername(), principal.GetId() ?? string.Empty, principal.GetEmail(), principal.GetRoles());
    }
}