using Checkout.PaymentGateway.Application.Payments.Service;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return "test";
        }
    }
}