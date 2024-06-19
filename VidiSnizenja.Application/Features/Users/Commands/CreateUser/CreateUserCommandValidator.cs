using FluentValidation;

namespace VidiSnizenja.Application.Features.Users.Commands.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.firstName)
                .NotEmpty()
                .WithMessage(CreateUserValidationMessages.FieldIsRequired);

        RuleFor(user => user.lastName)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired);

        RuleFor(user => user.username)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired);

        RuleFor(user => user.email)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired)
            .EmailAddress()
            .WithMessage(CreateUserValidationMessages.InvalidEmail);

        RuleFor(user => user.password)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired)
            .MinimumLength(8)
            .WithMessage(CreateUserValidationMessages.PasswordMinLength);

        RuleFor(user => user.confirmPassword)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired)
            .MinimumLength(8)
            .WithMessage(CreateUserValidationMessages.PasswordMinLength)
            .Equal(user => user.password)
            .WithMessage(CreateUserValidationMessages.ConfirmPasswordMatch);
    }
}

internal sealed class CreateUserValidationMessages
{
    public const string FieldIsRequired = "Filed is required!";
    public const string InvalidEmail = "Invalid email address!";
    public const string PasswordMinLength = "Password must conatin at least 8 characters!";
    public const string ConfirmPasswordMatch = "Password and confirm password doesn`t match!";
}