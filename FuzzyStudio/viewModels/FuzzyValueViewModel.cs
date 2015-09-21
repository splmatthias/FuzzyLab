using System.Collections.ObjectModel;

namespace fuzzyStudio.viewModels
{
    public class FuzzyValueViewModel : ViewModel
    {
        public FuzzyValueViewModel()
        {
            Values = new ObservableCollection<TermValueViewModel>();
        }

        public string FuzzyVariable
        {
            get { return _fuzzyVariable; }
            set { SetProperty(ref _fuzzyVariable, value, "FuzzyVariable"); }
        }

        public ObservableCollection<TermValueViewModel> Values { get; private set; }
        
        private string _fuzzyVariable;
    }
}
