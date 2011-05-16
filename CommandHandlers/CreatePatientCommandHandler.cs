using System;
using Commands;
using CommonDomain.Persistence;
using Domain;

namespace CommandHandlers
{
    public class CreatePatientCommandHandler : IHandles<CreatePatientCommand>
    {
        private IRepository _repository = null;

        public CreatePatientCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreatePatientCommand command)
        {
            var patient = Patient.CreateNew(
                command.Id,
                new PatientName(command.Name),
                new PatientStatus(command.Status),
                new Address(command.Street, command.City, command.State, command.Zip)
                );

            _repository.Save(patient, Guid.NewGuid(), null);
        }
    }
}
