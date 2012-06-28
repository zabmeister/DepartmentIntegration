using System;
using System.Threading;
using NServiceBus;
using Shipping.Messages;

namespace Shipping.Fedex.ProxyServer
{
    public class ShipToFedexHandler : IHandleMessages<ShipToFedex>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipToFedex message)
        {
            Random rnd = new Random();

            Thread.Sleep(rnd.Next(5000, 25000));

            Bus.Reply<FedexResponse>(f => f.FedexTrackingCode = Guid.NewGuid());
            
        }
    }
}
