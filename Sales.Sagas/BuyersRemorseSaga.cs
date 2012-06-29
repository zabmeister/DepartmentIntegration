using System;
using NServiceBus.Saga;
using Sales.Events;
using Sales.Messages;

namespace Sales.Sagas
{
    public class BuyersRemorseSaga : Saga<BuyersRemorseSagaData>,
        IAmStartedByMessages<PlaceOrder>,
        IAmStartedByMessages<CancelOrder>,
        IHandleTimeouts<BuyersRemorseTimeout>
    {     
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PlaceOrder>(s => s.OrderId, m => m.OrderId);
            ConfigureMapping<CancelOrder>(s => s.OrderId, m => m.OrderId);
        }

        //NOTE: RequestUtcTimeout also supports passing the message in, if you need access to the information
        //NOTE: There is no information needed because the order details are saved down in the database as they come in
        public void Handle(PlaceOrder message)
        {
            Console.WriteLine("Saga: PlaceOrder");

            Data.OrderId = message.OrderId;

            RequestUtcTimeout<BuyersRemorseTimeout>(TimeSpan.FromSeconds(10));
        }

        public void Handle(CancelOrder message)
        {
            Console.WriteLine("Saga: CancelOrder");

            Data.OrderCancelled = true;
        }

        public void Timeout(BuyersRemorseTimeout state)
        {
            Console.WriteLine("Saga: BuyersRemorseTimeout");

            if (!Data.OrderCancelled)
            {
                Console.WriteLine("Saga: Order Accepted");
                Bus.Publish<OrderAccepted>(m => m.OrderId = Data.OrderId);
            }

            MarkAsComplete();
        }
    }
}