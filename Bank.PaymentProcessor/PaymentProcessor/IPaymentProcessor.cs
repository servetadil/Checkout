using Bank.PaymentProcessor.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bank.PaymentProcessor.PaymentProcessor
{
    public interface IPaymentProcessor
    {
        Task<TransactionResponse> CreatePaymentTransactionAsync(Uri uri, object content);
    }
}
