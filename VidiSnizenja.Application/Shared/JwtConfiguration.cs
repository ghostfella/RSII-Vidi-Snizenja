using Microsoft.IdentityModel.Tokens;

namespace VidiSnizenja.Application.Shared;

public class JwtConfiguration
{
    public TimeSpan TokenDuration { get; }
    public TimeSpan RefreshTokenDuration { get; }
    public SymmetricSecurityKey SecurityKey { get; set; }
    public SigningCredentials SigningCredentials { get; set; }
    public string SecretKey = "j0#uZ@3h$8!mPq1*o";
}
