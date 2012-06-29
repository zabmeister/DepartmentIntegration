using System;

namespace Sales.Messages
{
    public class CancelOrder
    {
        public Guid OrderId { get; set; }       
    }
}