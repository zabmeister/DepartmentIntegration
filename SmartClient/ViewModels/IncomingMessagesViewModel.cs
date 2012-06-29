using System.Collections.ObjectModel;

namespace SmartClient.ViewModels
{
    public class IncomingMessagesViewModel 
    {
        public ObservableCollection<string> Messages { get; private set; }

        public IncomingMessagesViewModel()
        {
            Messages = new ObservableCollection<string> {"Started"};
        }
    }
}
