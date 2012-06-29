using System;
using NServiceBus;
using Shipping.Messages;

namespace Shipping.Server.Mine
{
    public class ShipToFedexHandler : IHandleMessages<ShipToFedex>
    {
        public void Handle(ShipToFedex message)
        {
            Console.WriteLine("ShipToFedex: " + message.OrderId);
        }
    }

    public class ShipToUpsHandler : IHandleMessages<ShipToUps>
    {
        public void Handle(ShipToUps message)
        {
            Console.WriteLine("ShipToUps: " + message.OrderId);
        }
    }
}