using Crm.Events;
using NServiceBus;
using SmartClient.CompositeEvents;

namespace SmartClient.Handlers.Crm
{
    public class CustomerStatusUpdatedHandler : IHandleMessages<CustomerStatusUpdated>
    {
        public void Handle(CustomerStatusUpdated message)
        {
            EventAggregatorProvider.Instance.GetEvent<CustomerStatusUpdatedEvent>().Publish(message);
        }
    }
}