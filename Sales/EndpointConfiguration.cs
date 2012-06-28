using NServiceBus;

namespace Sales.Server
{
    public class EndpointConfiguration : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"));
        }
    }
}
