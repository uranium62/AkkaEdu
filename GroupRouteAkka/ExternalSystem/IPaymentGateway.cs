namespace GroupRouteAkka.ExternalSystem
{
    interface IPaymentGateway
    {
        void Pay(int accountNumber, decimal amount);
    }
}
