using GalaSoft.MvvmLight;
using Reporting;

namespace PatientRecords.ViewModels
{
    public class PatientDetailsViewModel : ViewModelBase
    {
        private PatientDto _patient = null;

        /// <summary>
        /// Initializes a new instance of the PatientDetailsViewModel class.
        /// </summary>
        public PatientDetailsViewModel(string patientId, IReadRepository repository)
        {
            _patient = repository.GetById<PatientDto>(patientId);
        }

        public PatientDto Patient
        {
            get
            {
                return _patient;
            }
        }
    }
}