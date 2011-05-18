using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Infrastructure;

namespace PatientRecords.ViewModels
{
    public class ChangePatientNameViewModel : ViewModelBase
    {
        private IBus _bus = null;

        public ChangePatientNameViewModel(IBus bus)
        {
            _bus = bus;
        }


    }
}
