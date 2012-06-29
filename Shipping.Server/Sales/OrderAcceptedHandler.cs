using NServiceBus;
using Sales.Messages;
using Shipping.Messages;

namespace Shipping.Server.Sales
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderAccepted message)
        {
            Bus.Send<ShipOrder>(m => m.OrderId = message.OrderId);
        }
    }
}
