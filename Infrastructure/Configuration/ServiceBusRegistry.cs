using CommandHandlers;
using Commands;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using Infrastructure.Bus;
using Reporting;
using StructureMap.Configuration.DSL;


namespace Infrastructure.Configuration
{
    public class ServiceBusRegistry : Registry
    {
        public ServiceBusRegistry()
        {
            For<IReportingRepository<PatientDto>>()
                .Use<SqlReportingRepository>()
                .Ctor<string>()
                    .Is(AppConfig.PatientDBEventStoreEntities);

            var bus = new InProcessBus();

            For<IBus>().Use(bus);

            var eventStore = GetInitializedEventStore(bus);
            var repository = new EventStoreRepository(eventStore, new AggregateFactory(), new ConflictDetector());

            For<IStoreEvents>()
                .Use(eventStore);

            For<IRepository>()
                .Use(repository);

            For<IHandles<CreatePatientCommand>>()
                .Use<CreatePatientCommandHandler>();

            For<IHandles<ChangePatientNameCommand>>()
                .Use<ChangePatientNameCommandHandler>();
        }

        private IStoreEvents GetInitializedEventStore(IPublishMessages bus)
        {
            return Wireup.Init()
                .UsingSqlPersistence(AppConfig.SqlDBConnectionStringName)
                    //.InitializeStorageEngine()
                    .UsingJsonSerialization()
                        .Compress()
                .UsingSynchronousDispatcher(bus)
                .Build();

   //         .UsingSqlPersistence("EventStore")
            //	.InitializeStorageEngine()
            //	.UsingJsonSerialization()
            //		.Compress()
            //		.EncryptWith(EncryptionKey)
            //.HookIntoPipelineUsing(new[] { new AuthorizationPipelineHook() })
            //.UsingAsynchronousDispatcher()
            //	.PublishTo(new DelegateMessagePublisher(DispatchCommit))
            //.Build();
        }
    }
}
