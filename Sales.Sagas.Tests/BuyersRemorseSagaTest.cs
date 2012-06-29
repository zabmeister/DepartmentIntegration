using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus.Testing;
using Sales.Messages;

namespace Sales.Sagas.Tests
{

    [TestClass]
    public class BuyersRemorseSagaTest
    {
        [TestMethod]
        public void When_order_placed_should_wait_for_the_time_of_remorse()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var placeOrder = new PlaceOrder { OrderId = Guid.NewGuid(), Items = new List<string>{ "Bag", "Chocolate"}};

            Test.Saga<BuyersRemorseSaga>()
                .ExpectTimeoutToBeSetIn<BuyersRemorseTimeout>((to, ts) => ts == TimeSpan.FromSeconds(60))
                .When(s => s.Handle(placeOrder));
        }

        [TestMethod]
        public void When_remorse_timeout_elapsed_and_not_cancelled_should_publish_order_accepted()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var orderId = Guid.NewGuid();

            Test.Saga<BuyersRemorseSaga>()                
                .ExpectPublish<OrderAccepted>(e => e.OrderId == orderId)
                .When(s =>
                          {
                              s.Data.OrderId = orderId;
                              s.Timeout(new BuyersRemorseTimeout());
                          })
                .AssertSagaCompletionIs(true);

            //NOTE: Assert comes last becuase the AAA is mixed up
        }

        [TestMethod]
        public void When_remorse_timeout_elapsed_and_cancelled_should_not_publish_order_accepted()
        {
            //NOTE: This initializes e.g. the bus
            Test.Initialize();

            var orderId = Guid.NewGuid();

            Test.Saga<BuyersRemorseSaga>()
                .ExpectNotPublish<OrderAccepted>(e => e.OrderId == orderId)
                .When(s =>
                {
                    s.Data.OrderId = orderId;
                    s.Timeout(new BuyersRemorseTimeout());
                })
                .AssertSagaCompletionIs(true);

            //NOTE: Assert comes last becuase the AAA is mixed up
        }
    }
}
