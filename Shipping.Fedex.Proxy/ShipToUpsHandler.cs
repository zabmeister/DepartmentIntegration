using System;
using System.Threading;
using NServiceBus;
using Shipping.Messages;

namespace Shipping.ProxyServer
{
    public class ShipToUpsHandler : IHandleMessages<ShipToUps>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipToUps message)
        {
            Console.WriteLine("ProxyServer: ShipToUps");
         
            var rnd = new Random();

            Thread.Sleep(rnd.Next(1000, 40000));

            Bus.Reply<UpsResponse>(f => f.UpsTrackingCode = Guid.NewGuid());

            Console.WriteLine("ProxyServer: ShipToUps - DONE");
        }
    }
}