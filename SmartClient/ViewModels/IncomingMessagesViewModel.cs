using System.Collections.ObjectModel;
using Crm.Events;
using Microsoft.Practices.Prism.Events;
using SmartClient.CompositeEvents;

namespace SmartClient.ViewModels
{
    public class IncomingMessagesViewModel 
    {
        public ObservableCollection<string> Messages { get; private set; }

        public IncomingMessagesViewModel()
        {
            Messages = new ObservableCollection<string> {"Started"};
            EventAggregatorProvider.Instance.GetEvent<CustomerStatusUpdatedEvent>().Subscribe(OnCustomerStatusUpdatedEvent, ThreadOption.UIThread, true);
        }

        private void OnCustomerStatusUpdatedEvent(CustomerStatusUpdated message)
        {
            Messages.Add(string.Format("[{0}]: {1} - {2}", "CustomerStatusUpdated", message.CustomerId,
                                       message.CustomerStatus));
        }
    }
}
