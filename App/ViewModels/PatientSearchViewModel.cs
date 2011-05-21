using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Reporting;

namespace PatientRecords.ViewModels
{
    public class PatientSearchViewModel : ViewModelBase
    {
        private IReportingRepository<PatientDto> _repository = null;

        /// <summary>
        /// Initializes a new instance of the PatientSearchViewModel class.
        /// </summary>
        public PatientSearchViewModel(IReportingRepository<PatientDto> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// The <see cref="PatientResults" /> property's name.
        /// </summary>
        public const string PatientResultsPropertyName = "PatientResults";

        private ObservableCollection<PatientListViewModel> _patientResults = new ObservableCollection<PatientListViewModel>();

        /// <summary>
        /// Gets the PatientResults property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<PatientListViewModel> PatientResults
        {
            get
            {
                return _patientResults;
            }

            set
            {
                if (_patientResults == value)
                {
                    return;
                }

                _patientResults = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PatientResultsPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SearchText" /> property's name.
        /// </summary>
        public const string SearchTextPropertyName = "SearchText";

        private string _searchText = "";

        /// <summary>
        /// Gets the SearchText property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;

                RaisePropertyChanged(SearchTextPropertyName);
            }
        }


        private ICommand _search = null;

        public ICommand Search
        {
            get
            {
                if (_search == null)
                {
                    _search = new RelayCommand(
                        () =>
                        {
                            var results = _repository.GetAll(x => x.Name == SearchText);

                            if (results.Count() > 0)
                            {
                                _patientResults = new ObservableCollection<PatientListViewModel>
                                    (
                                        (from p in results
                                         select new PatientListViewModel(p)).ToList()
                                    );
                                RaisePropertyChanged(PatientResultsPropertyName);
                            }
                        }
                    );
                }

                return _search;
            }
        }

    }
}