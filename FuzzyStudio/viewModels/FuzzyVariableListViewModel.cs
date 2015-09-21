using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBase.viewModels;

namespace fuzzyStudio.viewModels
{
    public class FuzzyVariableListViewModel : ListViewModel<FuzzyVariableViewModel>
    {
        public FuzzyVariableListViewModel(NumericVariableListViewModel numericVariables)
        {
            if (numericVariables == null) 
                throw new ArgumentNullException("numericVariables");

            _numericVariables = numericVariables.Items;
            AddFuzzyVariable = new DelegateCommand(p =>
            {
                var fuzzyVariableViewModel = new FuzzyVariableViewModel(_numericVariables) {Identifier = "Fuzzy Variable " + ++_varCount};
                Items.Add(fuzzyVariableViewModel);
                SelectedItem = fuzzyVariableViewModel;
            });
            RemoveFuzzyVariable = new DelegateCommand(p => Items.Remove(p as FuzzyVariableViewModel));
        }

        public ICommand AddFuzzyVariable { get; private set; }

        public ICommand RemoveFuzzyVariable { get; private set; }

        private readonly ObservableCollection<NumericVariableViewModel> _numericVariables;
        private int _varCount;
    }
}
