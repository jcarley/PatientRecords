using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PatientRecords.ApplicationFramework;
using PatientRecords.ApplicationFramework.Events;
using StructureMap;

namespace PatientRecords.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Notifications.SearchForPatientMessage.Register(this, OnSearchForPatient);
            Notifications.ShowPatientDetailsMessage.Register(this, OnShowPatientDetails);
            Notifications.CreateNewPatientMessage.Register(this, OnCreateNewPatient);
        }

        public string Welcome
        {
            get
            {
                return "Patient Records v1.0";
            }
        }

        #region Commands

        private ICommand _customerSearch = null;

        public ICommand CustomerSearch
        {
            get
            {
                if (_customerSearch == null)
                {
                    _customerSearch = new RelayCommand(() =>
                    {
                        Notifications.SearchForPatientMessage.Send(new SearchForPatientEvent());
                    });
                }

                return _customerSearch;
            }
        }

        private ICommand _addNewCustomer = null;

        public ICommand AddNewCustomer
        {
            get
            {
                if (_addNewCustomer == null)
                {
                    _addNewCustomer = new RelayCommand(() =>
                    {
                        Notifications.CreateNewPatientMessage.Send(new CreateNewPatientEvent());
                    });
                }

                return _addNewCustomer;
            }
        }

        private ICommand _exit = null;

        public ICommand Exit
        {
            get
            {
                if (_exit == null)
                {
                    _exit = new RelayCommand(() =>
                    {
                        Application.Current.Shutdown();
                    });
                }

                return _exit;
            }
        }

        #endregion

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
            // can not replace the current active item with the same type of item
            if (_activeItem != null && (_activeItem as UserControl).DataContext is T)
                return;

            Type t = typeof(T);

            string name = t.Name;

            string viewName = name.Replace("Model", string.Empty);

            viewName = "PatientRecords.Views." + viewName;

            Type viewType = Type.GetType(viewName);

            UserControl control = Activator.CreateInstance(viewType) as UserControl;

            control.DataContext = viewModel;

            if (_activeItem != null)
            {
                IDisposable disposable = _activeItem as IDisposable;

                if (disposable != null)
                    disposable.Dispose();

                _activeItem = null;
            }

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

        private void OnSearchForPatient(SearchForPatientEvent evt)
        {
            var viewModel = ObjectFactory.GetInstance<PatientSearchViewModel>();
            ActivateItem(viewModel);
        }

        private void OnShowPatientDetails(ShowPatientDetailsEvent evt)
        {
            string patientId = evt.PatientId;

            var viewModel = ObjectFactory
                .With<string>(patientId)
                .GetInstance<PatientDetailsViewModel>();

            ActivateItem(viewModel);
        }

        private void OnCreateNewPatient(CreateNewPatientEvent evt)
        {
            var viewModel = ObjectFactory.GetInstance<CreatePatientViewModel>();
            ActivateItem(viewModel);
        }

        #endregion
    }
}