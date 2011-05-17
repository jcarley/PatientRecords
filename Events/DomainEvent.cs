using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    [Serializable]
    public class DomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid ClientId { get; set; }
    }
}
