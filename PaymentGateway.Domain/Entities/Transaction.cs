using PaymentGateway.Domain.Common;
using PaymentGateway.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Entities
{
    public class Transaction : Entity
    {
        public Guid TransactionID { get; set; }

        public string OrderId { get; set; }

        public Merchant Merchant { get; set; }

        public Money Amount { get; set; }

        public CreditCard Card { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool IsFutureTransaction { get; set; }

        public int TransactionStatus { get; set; }

        private Transaction()
        {

        }
    }
}
