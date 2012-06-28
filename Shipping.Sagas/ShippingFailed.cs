using System;

namespace Shipping.Sagas
{
    public class ShippingFailed
    {
        public Guid OrderId { get; set; }
    }
}