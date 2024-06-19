using FluentValidation;
using VidiSnizenja.Application.Features.Users.Commands.CreateUser;

namespace VidiSnizenja.Application.Features.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.id)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired);
    }
}
