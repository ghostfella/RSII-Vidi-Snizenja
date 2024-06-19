using MediatR;
using VidiSnizenja.Application.Features.Auth.Commands.Login;

namespace VidiSnizenja.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string token) : IRequest<Result<LoginCommandResponse>> { }