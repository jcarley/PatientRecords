using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
using Reporting;

namespace PatientRecords.ViewModels
{
    public class PatientListViewModel : ViewModelBase, IEquatable<PatientListViewModel>
    {
        private PatientDto _patient = null;

        public PatientListViewModel(PatientDto patient)
        {
            _patient = patient;
        }

        public string Name
        {
            get
            {
                return _patient.Name;
            }
        }

        public string Status
        {
            get
            {
                return _patient.Status;
            }
        }

        private ICommand _showDetails = null;

        public ICommand ShowDetails
        {
            get
            {
                if (_showDetails == null)
                {
                    _showDetails = new RelayCommand(
                        () =>
                        {
                            Notifications.ShowPatientDetailsMessage.Send(new ShowPatientDetailsEvent(_patient.AggregateRootId));
                        }
                    );
                }

                return _showDetails;
            }
        }

        public bool Equals(PatientListViewModel other)
        {
            return _patient.Id.Equals(other._patient.Id);
        }
    }
}
