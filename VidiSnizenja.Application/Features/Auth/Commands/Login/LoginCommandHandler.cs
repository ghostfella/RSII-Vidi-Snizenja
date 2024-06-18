using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VidiSnizenja.Application.Exceptions;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtConfiguration _tokenConfiguration;

    public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtConfiguration> tokenConfiguration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenConfiguration = tokenConfiguration.Value;
    }

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.FindByNameAsync(request.username);

        if (user is null && !user.IsDeleted)
        {
            throw new NotFoundException($"User with username: '{request.username}' not found!", typeof(User));
        }

        if (!await _signInManager.CanSignInAsync(user))
        {
            throw new NotFoundException("You haven`t confirmed your account!", typeof(User));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.password, true);

        if (!result.Succeeded)
        {
            throw new NotFoundException("Incorrect username or password!", typeof(User));
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        LoginCommandResponse authResponse = Create(claims, user, _tokenConfiguration);

        return Result.Ok(authResponse);
    }

    private LoginCommandResponse Create(List<Claim>? claims, User user, JwtConfiguration tokenConfiguration)
    {
        // Build access token
        var expiriation = DateTime.UtcNow.AddMinutes(5);

        var token = new JwtSecurityToken(
        issuer: null,
        audience: null,
            claims: claims,
            expires: expiriation,
            signingCredentials: tokenConfiguration.SigningCredentials);

        var accessToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiriation);

        // Build refresh token
        var expirationOffset = tokenConfiguration.RefreshTokenDuration;
        var expirationDateTime = DateTime.UtcNow.Add(expirationOffset);

        //var userRefreshToken = AddRefreshToken(user, expirationDateTime);

        //var refreshToken = new TokenResource(userRefreshToken.Id, expirationDateTime);
        var refreshToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiriation);

        // Create auth response
        var authResponse = new LoginCommandResponse(accessToken, refreshToken);

        return authResponse;
    }

    //RefreshToken AddRefreshToken(User user, DateTime expiriationDate)
    //{
    //    var randomNumber = new byte[32];
    //    using (var rng = RandomNumberGenerator.Create())
    //    {
    //        rng.GetBytes(randomNumber);
    //    }

    //    var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), Convert.ToBase64String(randomNumber), expiriationDate);

    //    User.Add(refreshToken);

    //    return refreshToken;
    //}
}
