using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.PaymentProcessor.Model
{
    public class TransactionResponse
    {
        public string TransactionID { get; set; }

        public int StatusCode { get; set; }
    }
}
