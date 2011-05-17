using System;
using CommonDomain.Core;
using Events;

namespace Domain
{
    public class Patient : AggregateBase<DomainEvent>
    {
        private PatientName _name;
        private PatientStatus _status;
        private Address _address;

        public Patient()
        {
        }

        private Patient(Guid id, Guid clientId, PatientName patientName, PatientStatus patientStatus, Address address)
        {
            RaiseEvent(new PatientCreatedEvent(id, clientId, patientName.Name, patientStatus.Status, address.Street, address.City, address.State, address.Zip));
        }

        public static Patient CreateNew(Guid id, Guid clientId, PatientName patientName, PatientStatus patientStatus, Address address)
        {
            return new Patient(id, clientId, patientName, patientStatus, address);
        }

        //Domain-Eventhandlers
        private void Apply(PatientCreatedEvent evt)
        {
            Id = evt.AggregateId;
            _name = new PatientName(evt.Name);
            _status = new PatientStatus(evt.Status);
            _address = new Address(evt.Street, evt.City, evt.State, evt.Zip);
        }
    }

    public class PatientName
    {
        public string Name { get; set; }

        public PatientName(string name)
        {
            Name = name;
        }
    }

    public class PatientStatus
    {
        public string Status { get; set; }

        public PatientStatus(string status)
        {
            Status = status;
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }
    }
}
