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

        private Patient(Guid id, PatientName patientName, PatientStatus patientStatus, Address address)
        {
            RaiseEvent(new PatientCreatedEvent(id, patientName.Name, patientStatus.Status, address.Street, address.City, address.State, address.Zip));
        }

        public static Patient CreateNew(Guid id, PatientName patientName, PatientStatus patientStatus, Address address)
        {
            return new Patient(id, patientName, patientStatus, address);
        }

        public void ChangeName(string name)
        {
            if (Id == Guid.Empty)
            {
                throw new NonExistingPatientException("The patient is not created and no operations can be executed on it.");
            }

            RaiseEvent(new PatientNameChanged(Id, name));
        }

        //Domain-Eventhandlers
        private void Apply(PatientCreatedEvent evt)
        {
            Id = evt.AggregateId;
            _name = new PatientName(evt.Name);
            _status = new PatientStatus(evt.Status);
            _address = new Address(evt.Street, evt.City, evt.State, evt.Zip);
        }

        private void Apply(PatientNameChanged evt)
        {
            _name = _name.Change(evt.Name);
        }
    }

    public class PatientName
    {
        public string Name { get; set; }

        public PatientName(string name)
        {
            Name = name;
        }

        public PatientName Change(string name)
        {
            return new PatientName(name);
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
