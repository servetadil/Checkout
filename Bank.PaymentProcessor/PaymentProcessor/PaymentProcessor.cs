using Bank.PaymentProcessor.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bank.PaymentProcessor.PaymentProcessor
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly HttpClient _httpClient;

        public PaymentProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TransactionResponse> CreatePaymentTransactionAsync(Uri uri, object content)
        {
            //*TODO */
            // 
            // MakeBankClientCall could be used for integrate with real bank on production.
            // This is commented for Mock this result.
            //  await MakeBankClientCall(uri, content);
            //

            return await Task.Run(() =>
            {
                return new TransactionResponse()
                {
                    TransactionID = Guid.NewGuid().ToString(),
                    StatusCode = TransactionStatusCode.PaymentProcessedSuccessfully
                };
            });
        }

        public async Task<TransactionResponse> MakeBankClientCall(Uri uri, object content)
        {
            try
            {
                var returnCode = TransactionStatusCode.PaymentFailed;

                var stringContent = new StringContent(
                 JsonConvert.SerializeObject(content), UnicodeEncoding.UTF8, "application/json");


                HttpResponseMessage result = await _httpClient.PostAsJsonAsync(uri, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    returnCode = TransactionStatusCode.PaymentProcessedSuccessfully;
                }

                return new TransactionResponse()
                {
                    TransactionID = Guid.NewGuid().ToString(),
                    StatusCode = returnCode
                };
            }
            catch (Exception ex)
            {
                return new TransactionResponse()
                {
                    TransactionID = Guid.NewGuid().ToString(),
                    StatusCode = TransactionStatusCode.PaymentFailed
                };
            }
        }
    }
}

