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
    public class ChangeNameCommand_TestFixture
    {
        [Test]
        public void ChangePatientNameCommand_should_change_Patient_Name()
        {
            Guid patientId = Guid.NewGuid();

            string originalName = "Jeff Carley";

            string expectedName = "Jefferson Carley";

            var patient = Patient.CreateNew(
                patientId,
                new PatientName(originalName),
                new PatientStatus("Active"),
                new Address("444 South Street", "Madison", "WI", "53701"));

            IRepository repository = MockRepository.GenerateMock<IRepository>();

            repository.Stub(r => r.GetById<Patient>(patientId, 0)).Return(patient);


            var commandHandler = new ChangePatientNameCommandHandler(repository);

            var command = new ChangePatientNameCommand(Guid.NewGuid(), patientId)
            {
                Name = expectedName
            };

            commandHandler.Handle(command);

            var args = repository.GetArgumentsForCallsMadeOn(r =>
                r.Save(Arg<Patient>.Is.Anything, Arg<Guid>.Is.Anything, Arg<Action<IDictionary<string, object>>>.Is.Null));

            var actualPatient = args[0][0] as Patient;
            var list = new ArrayList((actualPatient as IAggregate).GetUncommittedEvents());

            list.ShouldNotBeEmpty();

            PatientNameChangedEvent evt = null;
            foreach (var item in list)
            {
                if (item is PatientNameChangedEvent)
                {
                    evt = item as PatientNameChangedEvent;
                    break;
                }
            }

            evt.ShouldNotBeNull();
            evt.Name.ShouldEqual(expectedName);
        }
    }
}
