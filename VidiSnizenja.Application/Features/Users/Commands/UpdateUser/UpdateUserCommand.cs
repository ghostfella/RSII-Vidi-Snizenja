using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(string id, string firstName, string lastName, string email, DateTime birthdate) : IRequest<Result<UpdateUserCommandResponse>>;