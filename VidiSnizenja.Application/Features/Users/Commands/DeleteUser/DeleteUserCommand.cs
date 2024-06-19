using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(string id) : IRequest<Result<DeleteUserCommandResponse>>;
