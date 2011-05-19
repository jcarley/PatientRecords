using Events;
using Raven.Client;
using Reporting;

namespace EventHandlers
{
    public class PatientView : IHandlesEvent<PatientCreatedEvent>, IHandlesEvent<PatientNameChangedEvent>, IHandlesEvent<PatientRelocatedEvent>
    {
        private IDocumentStore _documentStore = null;

        public PatientView(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Handle(PatientCreatedEvent domainEvent)
        {
            var dto = new PatientDto()
            {
                AggregateRootId = domainEvent.AggregateId,
                Name = domainEvent.Name,
                Status = domainEvent.Status,
                Street = domainEvent.Street,
                City = domainEvent.City,
                State = domainEvent.State,
                Zip = domainEvent.Zip
            };

            using (var session = _documentStore.OpenSession())
            {
                session.Store(dto);
                session.SaveChanges();
            }
        }

        public void Handle(PatientNameChangedEvent domainEvent)
        {
            using (var session = _documentStore.OpenSession())
            {
                var dto = session.Load<PatientDto>(DtoBase.GetDtoIdOf<PatientDto>(domainEvent.AggregateId));
                dto.Name = domainEvent.Name;
                session.SaveChanges();
            }
        }

        public void Handle(PatientRelocatedEvent domainEvent)
        {
            using (var session = _documentStore.OpenSession())
            {
                var dto = session.Load<PatientDto>(DtoBase.GetDtoIdOf<PatientDto>(domainEvent.AggregateId));
                dto.Street = domainEvent.Street;
                dto.City = domainEvent.City;
                dto.State = domainEvent.State;
                dto.Zip = domainEvent.Zip;
                session.SaveChanges();
            }
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
