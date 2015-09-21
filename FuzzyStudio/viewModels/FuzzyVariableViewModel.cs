using System.Collections.ObjectModel;
using System.Windows.Input;

namespace fuzzyStudio.viewModels
{
    public class FuzzyVariableViewModel : ViewModel
    {
        public FuzzyVariableViewModel(ObservableCollection<NumericVariableViewModel> numericVariables)
        {
            NumericVariables = numericVariables;
            Terms = new ObservableCollection<TermViewModel>();

            createCommands();
        }

        public string Identifier
        {
            get { return _identifier; }
            set { SetProperty(ref _identifier, value, "Identifier"); }
        }

        public NumericVariableViewModel NumericVariable
        {
            get { return _numericVariable; }
            set { SetProperty(ref _numericVariable, value, "NumericVariable"); }
        }

        public ObservableCollection<NumericVariableViewModel> NumericVariables { get; private set; }

        public ObservableCollection<TermViewModel> Terms { get; private set; }
        
        public ICommand AddFuzzyTerm { get; private set; }

        public ICommand RemoveFuzzyTerm { get; private set; }

        private void createCommands()
        {
            AddFuzzyTerm = new DelegateCommand(p => Terms.Add(new TermViewModel("Term " + ++_termCount)));
            RemoveFuzzyTerm = new DelegateCommand(p => Terms.Remove(p as TermViewModel));
        }

        private string _identifier;
        private int _termCount;
        private NumericVariableViewModel _numericVariable;
    }
}
