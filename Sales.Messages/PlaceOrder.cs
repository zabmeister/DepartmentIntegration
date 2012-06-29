using System;
using System.Collections.Generic;

namespace Sales.Messages
{
    [Serializable]
    public class PlaceOrder
    {
        public Guid OrderId { get; set; }
        public List<string> Items { get; set; }
    }
}