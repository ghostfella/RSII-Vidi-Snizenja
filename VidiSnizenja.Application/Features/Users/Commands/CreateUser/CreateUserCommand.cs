using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string firstName, string lastName, string email, string username, DateTime Birthdate, string password, string confirmPassword) : IRequest<Result<CreateUserCommandResponse>>;