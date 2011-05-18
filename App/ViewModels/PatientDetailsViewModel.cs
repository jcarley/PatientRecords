using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
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

        private ICommand _done = null;

        public ICommand Done
        {
            get
            {
                if (_done == null)
                {
                    _done = new RelayCommand(() =>
                    {
                        Notifications.SearchForPatientMessage.Send(new SearchForPatientEvent());
                    });
                }
                return _done;
            }
        }

        private ICommand _relocatePatient = null;

        public ICommand RelocatePatient
        {
            get
            {
                if (_relocatePatient == null)
                {
                    _relocatePatient = new RelayCommand(() =>
                    {
                        // do something
                    });
                }

                return _relocatePatient;
            }
        }

        private ICommand _changePatientName = null;

        public ICommand ChangePatientName
        {
            get
            {
                if (_changePatientName == null)
                {
                    _changePatientName = new RelayCommand(() =>
                    {
                        // do something
                    });
                }

                return _changePatientName;
            }
        }
    }
}