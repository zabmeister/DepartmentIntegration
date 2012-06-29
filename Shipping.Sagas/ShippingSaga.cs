using System;
using NServiceBus;
using NServiceBus.Saga;
using Shipping.Messages;

namespace Shipping.Sagas
{
    public class ShippingSaga : Saga<ShippingSagaData>,
        IAmStartedByMessages<ShipOrder>, 
        IHandleMessages<FedexResponse>,
        IHandleTimeouts<FedexTimedout>,
        IHandleMessages<UpsResponse>,
        IHandleTimeouts<UpsTimeout>

    {
        public void Handle(ShipOrder message)
        {
            Console.WriteLine("Saga: ShipOrder");
            
            Data.OrderId = message.OrderId;
            Bus.Send<ShipToFedex>(s => s.OrderId = message.OrderId);
            RequestUtcTimeout<FedexTimedout>(TimeSpan.FromSeconds(15));
        }

        public void Handle(FedexResponse message)
        {
            Console.WriteLine("Saga: FedexResponse");
            MarkAsComplete();
            ReplyToOriginator<ShippingCompleted>(s =>
                                                     { 
                                                         s.ShippingCompany = ShippingCompanies.Fedex;
                                                         s.TrackingCode = message.FedexTrackingCode;
                                                     }
                                                );
        }

        public void Timeout(FedexTimedout state)
        {
            Console.WriteLine("Saga: FedexTimedout");

            Bus.Send<ShipToUps>(s => s.OrderId = Data.OrderId);
            RequestUtcTimeout<UpsTimeout>(TimeSpan.FromSeconds(30));
        }

        public void Handle(UpsResponse message)
        {
            Console.WriteLine("Saga: UpsResponse");

            MarkAsComplete();
            ReplyToOriginator<ShippingCompleted>(s =>
                                                     {
                                                         s.ShippingCompany = ShippingCompanies.Ups;
                                                         s.TrackingCode = message.UpsTrackingCode;
                                                     }
                );
        }

        public void Timeout(UpsTimeout state)
        {
            Console.WriteLine("Saga: UpsTimeout");

            MarkAsComplete();
            ReplyToOriginator<ShippingFailed>(s => s.OrderId = Data.OrderId);
        }
    }
}
