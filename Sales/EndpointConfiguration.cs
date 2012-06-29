using NServiceBus;

namespace Sales.Server
{
    public class EndpointConfiguration : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Sales.Messages"));
        }
    }
}
