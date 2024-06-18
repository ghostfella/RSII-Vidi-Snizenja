using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string username, string password) : IRequest<Result<LoginCommandResponse>>;
