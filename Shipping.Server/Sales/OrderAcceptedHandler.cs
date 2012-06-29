using System;
using NServiceBus;
using Sales.Events;
using Shipping.Messages;

namespace Shipping.Server.Sales
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderAccepted message)
        {
            Console.WriteLine("Order received, sending to shipping");
            Bus.SendLocal<ShipOrder>(m => m.OrderId = message.OrderId);
        }
    }
}
