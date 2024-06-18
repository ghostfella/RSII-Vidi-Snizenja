namespace VidiSnizenja.Application.Features.Auth.Commands.Login;

public sealed record LoginCommandResponse(TokenResource AccessToken, TokenResource RefreshToken);
public sealed record TokenResource(string Token, DateTime Expiration);