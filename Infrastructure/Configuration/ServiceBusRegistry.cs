using CommandHandlers;
using Commands;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;
using Infrastructure.Bus;
using Raven.Client;
using Raven.Client.Document;
using Reporting;
using StructureMap.Configuration.DSL;

namespace Infrastructure.Configuration
{
    public class ServiceBusRegistry : Registry
    {
        public ServiceBusRegistry()
        {
            For<IDocumentStore>()
                .Singleton()
                .Use(new DocumentStore { ConnectionStringName = AppConfig.ConnectionStringName })
                .OnCreation<IDocumentStore>(store => store.Initialize());

            var bus = new InProcessBus();

            For<IBus>().Use(bus);

            var eventStore = GetInitializedEventStore(bus);
            var repository = new EventStoreRepository(eventStore, new AggregateFactory(), new ConflictDetector());

            For<IStoreEvents>()
                .Use(eventStore);

            For<IRepository>()
                .Use(repository);

            For<IReadRepository>()
                .Use<RavenReadRepository>();

            For<IHandles<CreatePatientCommand>>()
                .Use<CreatePatientCommandHandler>();
        }

        private IStoreEvents GetInitializedEventStore(IPublishMessages bus)
        {
            return Wireup.Init()
                .UsingRavenPersistence(AppConfig.ConnectionStringName, new NullDocumentSerializer())
                .UsingSynchronousDispatcher(bus)
                .Build();
        }
    }
}
