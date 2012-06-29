using System;
using NServiceBus;
using Sales.Messages;

namespace Sales.Server
{
    public class ServerEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Console.WriteLine("Press 'p' to place a new order.");
            Console.WriteLine("Press 'c' to cancel order.");
            Console.WriteLine("To exit, Ctrl + C");
            
            var lastOrderGuid = new Guid();
            while (true)
            {
                var read = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(read))
                    break;

                if (string.Compare(read, "p", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    Guid guid = Guid.NewGuid();
                    Bus.SendLocal<PlaceOrder>(m => m.OrderId = guid);
                    lastOrderGuid = guid;
                    Console.WriteLine("Published event PlaceOrder: {0}", guid);
                }

                if (string.Compare(read, "c", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    Guid guid = lastOrderGuid;
                    Bus.SendLocal<CancelOrder>(m => m.OrderId = guid);
                    Console.WriteLine("Published event CancelOrder: {0}", guid);
                }
            }
        }

        public void Stop()
        {

        }
    }
}
