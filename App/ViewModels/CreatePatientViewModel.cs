using System;
using System.Windows.Input;
using Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            _command = new CreatePatientCommand(Guid.NewGuid());
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
                        Notifications.SearchForPatientMessage.Send(new SearchForPatientEvent());
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
                        Notifications.SearchForPatientMessage.Send(new SearchForPatientEvent());
                    });
                }
                return _cancelCreate;
            }
        }
    }
}
