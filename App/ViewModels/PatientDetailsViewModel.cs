using GalaSoft.MvvmLight;
using Reporting;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PatientRecords.ApplicationFramework.Events;
using PatientRecords.ApplicationFramework;

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

        private ICommand _done = null;

        public ICommand Done
        {
            get
            {
                if (_done == null)
                {
                    _done = new RelayCommand(() =>
                    {
                        Messenger.Default.Send(new SearchForPatientEvent(), Notifications.SearchForPatient);
                    });
                }
                return _done;
            }
        }
    }
}