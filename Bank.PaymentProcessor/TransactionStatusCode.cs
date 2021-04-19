namespace Bank.PaymentProcessor
{
    public class TransactionStatusCode
    {
        public const int PaymentProcessedSuccessfully = 201;

        public const int FuturePaymentTriggeredSuccessfully = 202;

        public const int PaymentFailed = 400;

    }
}