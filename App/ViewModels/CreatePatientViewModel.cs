using System;
using System.Windows.Input;
using Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infrastructure;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;

namespace PatientRecords.ViewModels
{
    public class CreatePatientViewModel : ViewModelBase
    {
        private IBus _bus = null;
        private CreatePatientCommand _command = null;

        public CreatePatientViewModel(IBus bus)
        {
            _bus = bus;
            _command = new CreatePatientCommand(Guid.NewGuid(), ClientIdentification.Id);
        }

        public CreatePatientCommand Command
        {
            get
            {
                return _command;
            }
        }

        private ICommand _createCustomer = null;

        public ICommand CreateCustomer
        {
            get
            {
                if (_createCustomer == null)
                {
                    _createCustomer = new RelayCommand(() =>
                    {
                        _bus.Send(_command);
                        Messenger.Default.Send(new SearchForPatientEvent(), Notifications.SearchForPatient);
                    });
                }
                return _createCustomer;
            }
        }

        private ICommand _cancelCreate = null;

        public ICommand CancelCreate
        {
            get
            {
                if (_cancelCreate == null)
                {
                    _cancelCreate = new RelayCommand(() =>
                    {
                        Messenger.Default.Send(new SearchForPatientEvent(), Notifications.SearchForPatient);
                    });
                }
                return _cancelCreate;
            }
        }
    }
}
