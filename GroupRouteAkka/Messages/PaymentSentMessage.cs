namespace GroupRouteAkka.Messages
{
    internal class PaymentSentMessage
    {
        public int AccountNumber { get; private set; }

        public PaymentSentMessage(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}
