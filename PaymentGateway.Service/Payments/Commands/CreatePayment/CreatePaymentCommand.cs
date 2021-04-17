using MediatR;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<string>
    {
    }
}
