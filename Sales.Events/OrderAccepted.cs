using System;

namespace Sales.Events
{
    public class OrderAccepted
    {
        public Guid OrderId { get; set; }
    }
}
