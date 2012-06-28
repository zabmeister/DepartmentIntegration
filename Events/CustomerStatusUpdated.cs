using System;

namespace Crm.Events
{
    public class CustomerStatusUpdated 
    {
        public Guid CustomerId { get; set; }
        public string CustomerStatus { get; set; }
    }
}
