using GalaSoft.MvvmLight;
using StructureMap;

namespace PatientRecords.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
        }

        /// <summary>
        /// Gets the Main property which defines the main viewmodel.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ObjectFactory.GetInstance<MainViewModel>();
            }
        }

        public PatientSearchViewModel PatientSearch
        {
            get
            {
                return ObjectFactory.GetInstance<PatientSearchViewModel>();
            }
        }


        public static void Cleanup()
        {
            
        }
    }
}
