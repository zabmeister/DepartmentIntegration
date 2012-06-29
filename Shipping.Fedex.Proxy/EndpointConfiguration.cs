using NServiceBus;

namespace Shipping.ProxyServer
{
    public class EndpointConfiguration : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Shipping.Messages"));
        }
    }
}
