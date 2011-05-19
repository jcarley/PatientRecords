using System;

namespace Events
{
    public class PatientNameChangedEvent : DomainEvent
    {
        public PatientNameChangedEvent(Guid id, string name)
        {
            AggregateId = id;
            Name = name;
        }
        
        public string Name { get; set; }
    }
}