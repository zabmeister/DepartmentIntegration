using SmartClient.ViewModels;

namespace SmartClient.Views
{
    public partial class IncomingMessagesView
    {
        public IncomingMessagesView()
        {
            InitializeComponent();

            DataContext = new IncomingMessagesViewModel();
        }
    }
}
