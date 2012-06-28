using System;
using NServiceBus.Saga;

namespace Shipping.Sagas
{
    public class ShippingSagaData : ISagaEntity
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        public Guid OrderId { get; set; }
    }
}