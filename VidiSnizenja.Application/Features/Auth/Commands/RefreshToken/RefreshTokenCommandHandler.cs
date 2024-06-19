using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using VidiSnizenja.Application.Features.Auth.Commands.Login;

namespace VidiSnizenja.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginCommandResponse>>
{
    private readonly IVidiSnizenjaDbContext _context;
    private readonly JwtConfiguration _tokenConfiguration;
    private readonly ICurrentUser _currentUser;

    public RefreshTokenCommandHandler(IDreamHomeDbContext context, IOptions<JwtConfiguration> tokenConfiguration, ICurrentUser currentUser)
    {
        _tokenConfiguration = tokenConfiguration.Value;
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Result<LoginCommandResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _context.Users.Include(u => u.RefreshTokens)
                                       .Include(u => u.Roles)
                                       .FirstOrDefaultAsync(u => u.Equals(_currentUser.Id), cancellationToken);

        if (user is null)
        {
            throw new Exception("Unknown error occurred!");
        }

        var token = user.RefreshTokens.FirstOrDefault(rt => rt.Id.ToString() == request.token);

        if (token is null || !token.IsValid)
        {
            throw new Exception("You haven`t confirmed your account!");
        }

        var roles = user.Roles.Select(u => u.Role.Name).ToList();

        var claims = new List<Claim>()
    {
        new Claim("Id", user.Id)
    };

        claims.AddRange(roles.Select(role => new Claim("Role", role)));

        LoginCommandResponse authResponse = Create(claims, user, _tokenConfiguration);

        token.MarkAsInvalid();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(authResponse);
    }

    private LoginCommandResponse Create(List<Claim>? claims, User user, JwtConfiguration tokenConfiguration)
    {
        // Build access token
        var expiration = DateTime.UtcNow.AddMinutes(5);

        var token = new JwtSecurityToken(
        issuer: null,
        audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: tokenConfiguration.SigningCredentials);

        var accessToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiration);

        // Build refresh token
        var expirationOffset = tokenConfiguration.RefreshTokenDuration;
        var expirationDateTime = DateTime.UtcNow.Add(expirationOffset);

        //var userRefreshToken = AddRefreshToken(user, expirationDateTime);

        //var refreshToken = new TokenResource(userRefreshToken.Id, expirationDateTime);
        var refreshToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiration);

        // Create auth response
        var authResponse = new LoginCommandResponse(accessToken, refreshToken);

        return authResponse;
    }
}
