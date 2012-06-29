using System;
using NServiceBus.Saga;

namespace Sales.Sagas
{
    public class BuyersRemorseSagaData : ISagaEntity
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public Guid OrderId { get; set; }
        public bool OrderCancelled { get; set; }
    }
}