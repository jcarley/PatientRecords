using System.Linq;
using EventHandlers;
using Events;
using StructureMap;
using Reporting;

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
            var reportingRepository = ObjectFactory.GetInstance<IReportingRepository<PatientDto>>();
            SetupEventHandlers(bus, reportingRepository);
        }

        private static void SetupEventHandlers(IBus bus, IReportingRepository<PatientDto> reportingRepository)
        {
            var patientEventHandler = new PatientView(reportingRepository);
            bus.RegisterHandler<PatientCreatedEvent>(patientEventHandler.Handle);
            bus.RegisterHandler<PatientNameChangedEvent>(patientEventHandler.Handle);
            bus.RegisterHandler<PatientRelocatedEvent>(patientEventHandler.Handle);

            //var patientEventPublisher = new PatientEventPublisher();
            //bus.RegisterHandler<PatientCreatedEvent>(patientEventPublisher.Handle);
        }
    }
}
