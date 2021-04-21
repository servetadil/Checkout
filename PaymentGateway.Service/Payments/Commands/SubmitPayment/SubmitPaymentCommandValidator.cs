using FluentValidation;

namespace Checkout.PaymentGateway.Application.Payments.Commands.SubmitPayment
{
    public class SubmitPaymentCommandValidator : AbstractValidator<SubmitPaymentCommand>
    {
        public SubmitPaymentCommandValidator()
        {
            RuleFor(x => x.CardName).MinimumLength(5).NotEmpty().NotNull();

            RuleFor(x => x.CardNumber).Length(16)
                .NotEmpty().NotNull().Matches("^[0-9]*$");

            RuleFor(x => x.CvvCode).NotEmpty().MinimumLength(3).
                MaximumLength(4).NotNull().Matches("^[0-9]*$");

            RuleFor(x => x.ExpiryMonth).LessThanOrEqualTo(12)
                .NotEmpty().NotNull();

            RuleFor(x => x.ExpiryYear).LessThanOrEqualTo(9999)
                .NotEmpty().NotNull();

            RuleFor(x => x.PaymentID).NotNull().NotEmpty();
        }
    }
}
