using System;
using System.Windows;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace SmartClient
{
    public partial class App
    {
        public static IBus Bus { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {            
            base.OnStartup(e);

            Bus = Configure.With()
                .DefineEndpointName("SmartClient-"+Guid.NewGuid())
                .Log4Net()
                .DefaultBuilder().DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Crm.Events"))
                .MsmqTransport()
                .UnicastBus()
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}
