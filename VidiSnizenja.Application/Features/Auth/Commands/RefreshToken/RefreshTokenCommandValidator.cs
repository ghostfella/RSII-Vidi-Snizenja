using FluentValidation;

namespace VidiSnizenja.Application.Features.Auth.Commands.RefreshToken;
public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(rtc => rtc.token)
            .NotEmpty()
            .WithMessage("Refresh token is missing!");
    }
}
