using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus.Testing;
using Shipping.Messages;

namespace Shipping.Sagas.Tests
{
    [TestClass]
    public class ShippingSagaTest
    {
        [TestMethod]
        public void When_shiporder_received_should_send_fedex_message()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var shipOrder = new ShipOrder {OrderId = Guid.NewGuid()};

            Test.Saga<ShippingSaga>()
                .ExpectSend<ShipToFedex>(s => s.OrderId == shipOrder.OrderId)
                .When(s => s.Handle(shipOrder));
        }

        [TestMethod]
        public void When_fedex_times_out_should_send_timeout()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var shipOrder = new ShipOrder { OrderId = Guid.NewGuid() };

            Test.Saga<ShippingSaga>()
                .ExpectTimeoutToBeSetIn<FedexTimedout>((f, t) =>t == TimeSpan.FromMinutes(15))
                .When(s => s.Handle(shipOrder));
        }

        [TestMethod]
        public void When_fedex_response_received_should_send_shipping_completed()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            Test.Saga<ShippingSaga>()
                .ExpectReplyToOrginator<ShippingCompleted>()
                .When(s => s.Handle(new FedexResponse()));
        }

        [TestMethod]
        public void When_ups_response_received_should_send_shipping_completed()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var upsResponse = new UpsResponse {UpsTrackingCode = Guid.NewGuid()};

            Test.Saga<ShippingSaga>()
                .ExpectReplyToOrginator<ShippingCompleted>(
                    s => s.ShippingCompany == ShippingCompanies.Ups && 
                         s.TrackingCode == upsResponse.UpsTrackingCode
                )
                .When(s => s.Handle(upsResponse));
        }

        [TestMethod]
        public void When_fedex_timeout_should_send_to_ups()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            Test.Saga<ShippingSaga>()
                .ExpectSend<ShipToUps>()
                .When(s => s.Timeout(new FedexTimedout()));
        }  

        [TestMethod]
        public void When_ups_timeout_should_send_shipping_failed()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            Test.Saga<ShippingSaga>()
                .ExpectReplyToOrginator<ShippingFailed>()
                .When(s => s.Timeout(new UpsTimeout()));
        }  
    }
}
