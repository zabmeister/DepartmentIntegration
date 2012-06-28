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
            Data.OrderId = message.OrderId;
            Bus.Send<ShipToFedex>(s => s.OrderId = message.OrderId);
            RequestUtcTimeout<FedexTimedout>(TimeSpan.FromMinutes(60));
        }

        public void Handle(FedexResponse message)
        {
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
            Bus.Send<ShipToUps>(s => s.OrderId = Data.OrderId);
            RequestUtcTimeout<UpsTimeout>(TimeSpan.FromMinutes(30));
        }

        public void Handle(UpsResponse message)
        {
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
            ReplyToOriginator<ShippingFailed>(s => s.OrderId = Data.OrderId);
        }
    }
}
