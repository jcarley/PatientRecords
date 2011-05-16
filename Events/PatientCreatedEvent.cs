using System;

namespace Events
{
    public class PatientCreatedEvent : DomainEvent
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public PatientCreatedEvent(Guid id, string name, string status, string street, string city, string state, string zip)
        {
            AggregateId = id;
            Name = name;
            Status = status;
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }
    }
}
