using System;

namespace Events
{
    [Serializable]
    public class DomainEvent
    {
        public Guid AggregateId { get; set; }
    }
}
