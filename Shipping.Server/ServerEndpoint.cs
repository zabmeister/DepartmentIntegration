using System;
using NServiceBus;
using Shipping.Messages;

namespace Shipping.Server
{
    public class ServerEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Console.WriteLine("Press 'Enter' to publish a message.To exit, Ctrl + C");

            var i = 0;
            while (Console.ReadLine() != null)
            {
                var eventMessage = new ShipOrder { OrderId = Guid.NewGuid()};

                Bus.Publish(eventMessage);

                Console.WriteLine("Published event ShipOrder: {0}", eventMessage.OrderId);

                i++;
            }
        }

        public void Stop()
        {

        }
    }
}
