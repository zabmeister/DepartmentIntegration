using NServiceBus;

namespace Crm.Server
{
    public class EndpointConfiguration : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"));
        }
    }
}
