using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.PaymentProcessor.Model
{
    public class TransactionRequest
    {
        public string PaymentTrackID { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public int CardMonth { get; set; }

        public int CardYear { get; set; }

        public int Cvv { get; set; }
    }
}
