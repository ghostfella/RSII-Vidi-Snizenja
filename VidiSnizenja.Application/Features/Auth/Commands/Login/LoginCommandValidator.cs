using FluentValidation;

namespace VidiSnizenja.Application.Features.Auth.Commands.Login;
public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(lc => lc.username)
           .NotEmpty()
           .WithMessage("Username is required!");

        RuleFor(lc => lc.password)
            .NotEmpty()
            .MinimumLength(LoginCommandOptions.PasswordLength)
            .WithMessage($"Password must contain at least {LoginCommandOptions.PasswordLength} characters!");
    }
}