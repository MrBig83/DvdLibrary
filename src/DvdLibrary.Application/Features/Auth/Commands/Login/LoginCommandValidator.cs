using FluentValidation;

namespace DvdLibrary.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginRequest.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LoginRequest.Password)
            .NotEmpty()
            .MinimumLength(4);
    }
}
