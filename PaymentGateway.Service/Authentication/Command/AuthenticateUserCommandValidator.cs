using Checkout.PaymentGateway.Application.Authentication.Command;
using FluentValidation;

namespace Checkout.PaymentGateway.Application.Authentication.Command
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(x => x.MerchantID)
                .MinimumLength(5)
                .MaximumLength(36)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ApiKey)
                .NotEmpty()
                .MaximumLength(36)
                .MinimumLength(36);
        }
    }
}
