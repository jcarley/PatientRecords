using System;

namespace Events
{
    public class PatientNameChanged : DomainEvent
    {
        public PatientNameChanged(Guid id, string name)
        {
            AggregateId = id;
            Name = name;
        }
        
        public string Name { get; set; }
    }
}
