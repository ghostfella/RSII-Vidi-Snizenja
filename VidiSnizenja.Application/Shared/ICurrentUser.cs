using System.Security.Claims;

namespace VidiSnizenja.Application.Shared;

public interface ICurrentUser
{
    string UserName { get; }

    string Id { get; }

    string Email { get; }

    string[] Roles { get; }

    Claim FindClaim(string claimType);

    Claim[] FindClaims(string claimType);

    Claim[] GetAllClaims();

    bool IsInRole(string roleName);

    bool IsInRoleAny(params string[] roleNames);
}
