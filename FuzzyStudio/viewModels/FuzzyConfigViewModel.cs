using System.Collections.ObjectModel;
using System.Windows.Input;

namespace fuzzyStudio.viewModels
{
    public class FuzzyConfigViewModel : ViewModel
    {
        public FuzzyConfigViewModel()
        {
            FuzzyRules = new ObservableCollection<FuzzyRuleViewModel>();
            FuzzyVariables = new ObservableCollection<FuzzyVariableViewModel>();
            NumericVariables = new NumericVariableListViewModel();
            Iterations = new ObservableCollection<ViewModel>();

            createCommands();
        }

        public ObservableCollection<ViewModel> Iterations { get; private set; }

        public ObservableCollection<FuzzyRuleViewModel> FuzzyRules { get; private set; }

        public ObservableCollection<FuzzyVariableViewModel> FuzzyVariables { get; private set; }

        public NumericVariableListViewModel NumericVariables { get; private set; }

        public ICommand AddFuzzyRule { get; private set; }

        public ICommand RemoveFuzzyRule { get; private set; }

        private void createCommands()
        {
            AddFuzzyRule = new DelegateCommand(p => FuzzyRules.Add(new FuzzyRuleViewModel {Identifier = "New Fuzzy Rule"}));
            RemoveFuzzyRule = new DelegateCommand(p => FuzzyRules.Remove(p as FuzzyRuleViewModel));
        }
    }
}
