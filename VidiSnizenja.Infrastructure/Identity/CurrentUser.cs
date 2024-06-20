using System.Security.Claims;
using VidiSnizenja.Application.Shared;

namespace VidiSnizenja.Infrastructure.Identity;

public sealed class CurrentUser : ICurrentUser
{
    public string UserName { get; }

    public string Id { get; }

    public string Email { get; }

    public string[] Roles { get; }

    public CurrentUser(string username, string id, string email, string[] roles)
    {
        UserName = username;
        Id = id;
        Email = email;
        Roles = roles;
    }


    public Claim FindClaim(string claimType)
    {
        throw new NotImplementedException();
    }

    public Claim[] FindClaims(string claimType)
    {
        throw new NotImplementedException();
    }

    public Claim[] GetAllClaims()
    {
        throw new NotImplementedException();
    }

    public bool IsInRole(string roleName)
    {
        throw new NotImplementedException();
    }

    public bool IsInRoleAny(params string[] roleNames)
    {
        throw new NotImplementedException();
    }
}