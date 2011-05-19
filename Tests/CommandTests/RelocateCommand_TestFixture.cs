using System;
using System.Collections;
using System.Collections.Generic;
using CommandHandlers;
using Commands;
using CommonDomain;
using CommonDomain.Persistence;
using Domain;
using Events;
using NUnit.Framework;
using Rhino.Mocks;
using Should;

namespace Tests.CommandTests
{
    [TestFixture]
    public class RelocateCommand_TestFixture
    {
        [Test]
        public void RelocateCommand_should_change_Patient_Address()
        {
            Guid patientId = Guid.NewGuid();

            string originalStreet = "444 South Street";
            string orginalCity = "Madison";
            string orginalState = "WI";
            string orginalZip = "53701";

            string expectedStreet = "123 Main St.";
            string expectedCity = "Windsor";
            string expectedState = "WI";
            string expectedZip = "53598";

            var patient = Patient.CreateNew(
                patientId,
                new PatientName("Jeff Carley"),
                new PatientStatus("Active"),
                new Address(originalStreet, orginalCity, orginalState, orginalZip));

            IRepository repository = MockRepository.GenerateMock<IRepository>();

            repository.Stub(r => r.GetById<Patient>(patientId, 0)).Return(patient);


            var commandHandler = new RelocatePatientCommandHandler(repository);

            var command = new RelocatePatientCommand(Guid.NewGuid(), patientId)
            {
                Street = expectedStreet,
                City = expectedCity,
                State = expectedState,
                Zip = expectedZip
            };

            commandHandler.Handle(command);

            var args = repository.GetArgumentsForCallsMadeOn(r =>
                r.Save(Arg<Patient>.Is.Anything, Arg<Guid>.Is.Anything, Arg<Action<IDictionary<string, object>>>.Is.Null));

            var actualPatient = args[0][0] as Patient;
            var list = new ArrayList((actualPatient as IAggregate).GetUncommittedEvents());

            list.ShouldNotBeEmpty();

            PatientRelocatedEvent evt = null;
            foreach (var item in list)
            {
                if (item is PatientRelocatedEvent)
                {
                    evt = item as PatientRelocatedEvent;
                    break;
                }
            }

            evt.ShouldNotBeNull();
            evt.Street.ShouldEqual(expectedStreet);
            evt.City.ShouldEqual(expectedCity);
            evt.State.ShouldEqual(expectedState);
            evt.Zip.ShouldEqual(expectedZip);
        }
    }
}
