using System;
using CommonDomain.Core;
using Events;
using CommonDomain;

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

        /// <summary>
        /// Creates a new patient record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patientName"></param>
        /// <param name="patientStatus"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Patient CreateNew(Guid id, PatientName patientName, PatientStatus patientStatus, Address address)
        {
            return new Patient(id, patientName, patientStatus, address);
        }

        /// <summary>
        /// Changes the name of the patient
        /// </summary>
        /// <param name="name"></param>
        public void ChangeName(string name)
        {
            if (Id == Guid.Empty)
            {
                throw new NonExistingPatientException("The patient is not created and no operations can be executed on it.");
            }

            RaiseEvent(new PatientNameChangedEvent(Id, name));
        }

        /// <summary>
        /// Sets the address of where the patient has relocated too.
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        public void Relocate(string street, string city, string state, string zip)
        {
            if (Id == Guid.Empty)
            {
                throw new NonExistingPatientException("The patient is not created and no operations can be executed on it.");
            }

            RaiseEvent(new PatientRelocatedEvent(Id, street, city, state, zip));
        }

        //Domain-Eventhandlers
        private void Apply(PatientCreatedEvent evt)
        {
            Id = evt.AggregateId;
            _name = new PatientName(evt.Name);
            _status = new PatientStatus(evt.Status);
            _address = new Address(evt.Street, evt.City, evt.State, evt.Zip);
        }

        private void Apply(PatientNameChangedEvent evt)
        {
            _name = _name.Change(evt.Name);
        }

        private void Apply(PatientRelocatedEvent evt)
        {
            _address = new Address(evt.Street, evt.City, evt.State, evt.Zip);
        }
    }

    public class PatientName
    {
        public string Name { get; private set; }

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
        public string Status { get; private set; }

        public PatientStatus(string status)
        {
            Status = status;
        }
    }

    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Address(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }
    }
}
