using StructureMap.Configuration.DSL;
using PatientRecords.Views;

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
