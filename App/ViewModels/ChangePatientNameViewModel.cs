using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Infrastructure;
using Reporting;

namespace PatientRecords.ViewModels
{
    public class ChangePatientNameViewModel : ViewModelBase
    {
        private IBus _bus = null;
        private IReadRepository _repository = null;

        public ChangePatientNameViewModel(IBus bus, IReadRepository repository)
        {
            _bus = bus;
            _repository = repository;
        }


    }
}
