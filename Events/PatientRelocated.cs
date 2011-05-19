using System;

namespace Events
{
    public class PatientRelocatedEvent : DomainEvent
    {
        public PatientRelocatedEvent(Guid id, string street, string city, string state, string zip)
        {
            AggregateId = id;
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
