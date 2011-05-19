using System;
using Commands;
using CommonDomain.Persistence;
using Domain;

namespace CommandHandlers
{
    public class RelocatePatientCommandHandler : IHandles<RelocatePatientCommand>
    {
        private IRepository _repository = null;

        public RelocatePatientCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(RelocatePatientCommand command)
        {
            var patient = _repository.GetById<Patient>(command.PatientId, 0);
            patient.Relocate(command.Street, command.City, command.State, command.Zip);
            _repository.Save(patient, Guid.NewGuid(), null);
        }
    }
}
