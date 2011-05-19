using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
using Reporting;
using System;

namespace PatientRecords.ViewModels
{
    public class PatientDetailsViewModel : ViewModelBase
    {
        private PatientDto _patient = null;

        /// <summary>
        /// Initializes a new instance of the PatientDetailsViewModel class.
        /// </summary>
        public PatientDetailsViewModel(Guid patientId, IReadRepository repository)
        {
            _patient = repository.GetById<PatientDto>(DtoBase.GetDtoIdOf<PatientDto>(patientId));
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
                        Notifications.ChangePatientNameMessage.Send(new ChangePatientNameEvent(_patient.AggregateRootId));
                    });
                }

                return _changePatientName;
            }
        }
    }
}