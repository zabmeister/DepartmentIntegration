using System;
using Crm.Events;
using NServiceBus;

namespace Crm.Server
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
                var eventMessage = new CustomerStatusUpdated { CustomerId = Guid.NewGuid(), CustomerStatus = "New Status"};

                Bus.Publish(eventMessage);

                Console.WriteLine("Published event CustomerStatusUpdated: {0}, {1}.", eventMessage.CustomerId, eventMessage.CustomerStatus);

                i++;
            }
        }

        public void Stop()
        {

        }
    }
}
