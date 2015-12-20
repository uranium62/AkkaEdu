﻿using System.Threading;

namespace GroupRouteAkka.ExternalSystem
{
    class DemoPaymentGateway : IPaymentGateway
    {
        public void Pay(int accountNumber, decimal amount)
        {
            // Simulate communicating with external payment gateway
            Thread.Sleep(200);
        }
    }
}
