using StructureMap.Configuration.DSL;

namespace PatientRecords.ViewModels
{
    public class ViewModelRegistry : Registry
    {
        public ViewModelRegistry()
        {
            For<MainViewModel>()
                .Singleton()
                .Use<MainViewModel>();

            ForConcreteType<PatientSearchViewModel>();

            ForConcreteType<PatientDetailsViewModel>();

            ForConcreteType<CreatePatientViewModel>();
        }
    }
}
