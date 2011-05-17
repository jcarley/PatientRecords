using Events;
using Raven.Client;
using Reporting;

namespace EventHandlers
{
    public class PatientView : IHandlesEvent<PatientCreatedEvent>
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
    }

    public class PatientEventPublisher : IHandlesEvent<PatientCreatedEvent>
    {
        public void Handle(PatientCreatedEvent domainEvent)
        {
            
        }
    }
}
