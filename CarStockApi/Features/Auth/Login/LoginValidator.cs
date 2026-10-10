// Defines validation rules for dealer login

using FastEndpoints;
using FluentValidation;

namespace CarStockApi.Features.Auth.Login;

public class LoginValidator : Validator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .Matches(@"^[\p{L}\p{N}_]+$")
            .WithMessage("Username can only contain letters and numbers.")
            .When(x => !string.IsNullOrWhiteSpace(x.Username));

        RuleFor(x => x)
            .Must(x =>
                !string.IsNullOrWhiteSpace(x.Username) &&
                !string.IsNullOrWhiteSpace(x.Password))
            .WithMessage("Username and Password are required.");
    }
}