using System.Windows.Input;
using WpfBase.viewModels;

namespace fuzzyStudio.viewModels
{
    public class NumericVariableListViewModel : ListViewModel<NumericVariableViewModel>
    {
        public NumericVariableListViewModel()
        {
            AddNumericVariable =
                new DelegateCommand(
                    p => Items.Add(new NumericVariableViewModel {Identifier = "Numeric Variable " + ++_varCount}));
            RemoveNumericVariable = new DelegateCommand(p => Items.Remove(p as NumericVariableViewModel));
        }

        public ICommand AddNumericVariable { get; private set; }

        public ICommand RemoveNumericVariable { get; private set; }

        private int _varCount;
    }
}
