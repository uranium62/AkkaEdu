using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteAkka.ExternalSystem
{
    interface IPaymentGateway
    {
        void Pay(int accountNumber, decimal amount);
    }
}
