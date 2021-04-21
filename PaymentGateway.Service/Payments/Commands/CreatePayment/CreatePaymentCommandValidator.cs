using FluentValidation;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderID).Length(5,36).NotEmpty().NotNull();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty().MaximumLength(3).MinimumLength(3);
        }
    }
}
