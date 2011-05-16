using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
using StructureMap;

namespace PatientRecords.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private IReadRepository _repository = null;
        //private IBus _bus = null;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //_repository = repository;
            //_bus = bus;

            Messenger.Default.Register<ShowPatientDetailsEvent>(this, Notifications.ShowPatientDetails, OnShowPatientDetails);
            Messenger.Default.Register<CreateNewPatientEvent>(this, Notifications.CreateNewPatient, OnCreateNewPatient);
        }

        public string Welcome
        {
            get
            {
                return "Patient Records v1.0";
            }
        }

        #region Activate Item

        /// <summary>
        /// The <see cref="ActiveItem" /> property's name.
        /// </summary>
        public const string ActiveItemPropertyName = "ActiveItem";

        private object _activeItem = null;

        /// <summary>
        /// Gets the ActiveItem property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public object ActiveItem
        {
            get
            {
                return _activeItem;
            }
        }
        
        public void ActivateItem<T>(T viewModel)
            where T : ViewModelBase
        {
            Type t = typeof(T);

            string name = t.Name;

            string viewName = name.Replace("Model", string.Empty);

            viewName = "PatientRecords.Views." + viewName;

            Type viewType = Type.GetType(viewName);

            UserControl control = Activator.CreateInstance(viewType) as UserControl;

            control.DataContext = viewModel;

            _activeItem = control;

            // Update bindings, no broadcast
            RaisePropertyChanged(ActiveItemPropertyName);
        }

        public void ActivateItem<T>()
            where T : ViewModelBase
        {
            T viewModel = ObjectFactory.GetInstance<T>();
            ActivateItem(viewModel);
        }

        #endregion

        #region Event Handlers

        private void OnShowPatientDetails(ShowPatientDetailsEvent evt)
        {
            string patientId = evt.PatientId;
            //var viewModel = new PatientDetailsViewModel(patientId, _repository);

            var viewModel = ObjectFactory
                .With<string>(patientId)
                .GetInstance<PatientDetailsViewModel>();

            ActivateItem(viewModel);
        }

        private void OnCreateNewPatient(CreateNewPatientEvent evt)
        {
            //var viewModel = new CreatePatientViewModel(_bus);
            var viewModel = ObjectFactory.GetInstance<CreatePatientViewModel>();
            ActivateItem(viewModel);
        }

        #endregion
    }
}