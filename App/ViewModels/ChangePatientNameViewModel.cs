using System;
using System.Windows.Input;
using Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Infrastructure;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
using Reporting;

namespace PatientRecords.ViewModels
{
    public class ChangePatientNameViewModel : ViewModelBase
    {
        private IBus _bus = null;
        private IReadRepository _repository = null;

        public ChangePatientNameViewModel(Guid patientId, IBus bus, IReadRepository repository)
        {
            _bus = bus;
            _repository = repository;
            _patient = _repository.GetById<PatientDto>(DtoBase.GetDtoIdOf<PatientDto>(patientId));

            if (_patient != null)
            {
                _command = new ChangePatientNameCommand(Guid.NewGuid(), _patient.AggregateRootId);
                _command.Name = _patient.Name;
            }
        }

        private ChangePatientNameCommand _command = null;

        public ChangePatientNameCommand Command
        {
            get
            {
                return _command;
            }
        }

        private PatientDto _patient;

        public PatientDto Patient
        {
            get
            {
                return _patient;
            }
        }
        

        private ICommand _save = null;
        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(() =>
                    {
                        _bus.Send(Command);
                        Notifications.ShowPatientDetailsMessage.Send(new ShowPatientDetailsEvent(_patient.AggregateRootId));
                    });
                }

                return _save;
            }
        }

        private ICommand _cancel = null;
        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(() =>
                    {
                        Notifications.ShowPatientDetailsMessage.Send(new ShowPatientDetailsEvent(_patient.AggregateRootId));
                    });
                }

                return _cancel;
            }
        }
    }
}
