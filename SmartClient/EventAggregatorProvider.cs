using Microsoft.Practices.Prism.Events;

namespace SmartClient
{
    public class EventAggregatorProvider
    {
        private static IEventAggregator _instance;
        public static IEventAggregator Instance 
        { 
            get
            {
                if (_instance == null)
                    _instance = new EventAggregator();

                return _instance;
            }
        }
    }
}
