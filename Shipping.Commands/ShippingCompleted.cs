using System;

namespace Shipping.Messages
{
    public class ShippingCompleted
    {
        public string ShippingCompany { get; set; }
        public Guid TrackingCode { get; set; }
    }
}
