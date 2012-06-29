using NServiceBus;

namespace Shipping.Server
{
    public class EndpointConfiguration : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Shipping.Messages"))  
                     .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Sales.Events"));
        }
    }
}
