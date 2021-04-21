using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentResultWm
    {
        public Guid OrderID { get; set; }
    }
}
