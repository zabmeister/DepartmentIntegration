using System;
using Crm.Events;
using NServiceBus;

namespace Sales.Server.Handlers.Crm
{
    public class CustomerStatusUpdatedHandler : IHandleMessages<CustomerStatusUpdated>
    {
        public void Handle(CustomerStatusUpdated message)
        {
            Console.WriteLine("Message received CustomerStatusUpdated: {0}, {1}", message.CustomerId, message.CustomerStatus);
        }
    }
}