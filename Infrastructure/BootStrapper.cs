using System.Linq;
using EventHandlers;
using Events;
using Raven.Client;
using StructureMap;

namespace Infrastructure
{
    public static class BootStrapper
    {
        static BootStrapper()
        {
            ConfigureContainer();
        }

        public static void Run()
        {
            ObjectFactory.GetAllInstances<ITask>()
                .ToList()
                .ForEach(tsk => tsk.Execute());
        }

        private static void ConfigureContainer()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Scan(ctx =>
                        {
                            ctx.AssembliesFromApplicationBaseDirectory();
                            ctx.AddAllTypesOf<ITask>();
                            ctx.LookForRegistries();
                        });
                });

            IBus bus = ObjectFactory.GetInstance<IBus>();
            IDocumentStore documentStore = ObjectFactory.GetInstance<IDocumentStore>();
            SetupEventHandlers(bus, documentStore);
        }

        private static void SetupEventHandlers(IBus bus, IDocumentStore documentStore)
        {
            var patientEventHandler = new PatientView(documentStore);
            bus.RegisterHandler<PatientCreatedEvent>(patientEventHandler.Handle);

            var patientEventPublisher = new PatientEventPublisher();
            bus.RegisterHandler<PatientCreatedEvent>(patientEventPublisher.Handle);
        }
    }
}
