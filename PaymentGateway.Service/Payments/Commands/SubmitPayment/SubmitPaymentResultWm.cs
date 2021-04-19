using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class SubmitPaymentResultWm
    {
        public int ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string OrderID { get; set; }

    }
}
