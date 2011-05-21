using Events;
using Reporting;

namespace EventHandlers
{
    public class PatientView : IHandlesEvent<PatientCreatedEvent>, IHandlesEvent<PatientNameChangedEvent>, IHandlesEvent<PatientRelocatedEvent>
    {
        private IReportingRepository<PatientDto> _reportingRepository = null;

        public PatientView(IReportingRepository<PatientDto> reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Handle(PatientCreatedEvent domainEvent)
        {
            var dto = new PatientDto()
            {
                Id = domainEvent.AggregateId,
                Name = domainEvent.Name,
                Status = domainEvent.Status,
                Street = domainEvent.Street,
                City = domainEvent.City,
                State = domainEvent.State,
                Zip = domainEvent.Zip
            };

            _reportingRepository.Insert(dto);
        }

        public void Handle(PatientNameChangedEvent domainEvent)
        {
            PatientDto dto = _reportingRepository.GetById(domainEvent.AggregateId);
            dto.Name = domainEvent.Name;
            _reportingRepository.Update(dto);
        }

        public void Handle(PatientRelocatedEvent domainEvent)
        {
            PatientDto dto = _reportingRepository.GetById(domainEvent.AggregateId);
            dto.Street = domainEvent.Street;
            dto.City = domainEvent.City;
            dto.State = domainEvent.State;
            dto.Zip = domainEvent.Zip;
            _reportingRepository.Update(dto);
        }
    }

    //public class PatientEventPublisher : IHandlesEvent<PatientCreatedEvent>
    //{
    //    public void Handle(PatientCreatedEvent domainEvent)
    //    {
    //        
    //    }
    //}
}
