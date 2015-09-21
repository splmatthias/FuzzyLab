using System.Collections.ObjectModel;

namespace fuzzyStudio.viewModels
{
    public class ScopeViewModel : ViewModel
    {
        public ScopeViewModel()
        {
            Values = new ObservableCollection<FuzzyValueViewModel>();
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, "Title"); }
        }

        public ObservableCollection<FuzzyValueViewModel> Values { get; private set; }
        
        private string _title;
    }
}
