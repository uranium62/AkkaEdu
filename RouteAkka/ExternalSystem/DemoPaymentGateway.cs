using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RouteAkka.ExternalSystem
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
