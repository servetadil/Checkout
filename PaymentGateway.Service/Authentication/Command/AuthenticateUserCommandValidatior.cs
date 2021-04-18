using Checkout.PaymentGateway.Application.Authentication.Command;
using FluentValidation;

namespace Checkout.PaymentGateway.Application.Authentication.Command
{
    public class AuthenticateUserCommandValidatior : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidatior()
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
