using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class FuzzyVariableListVm : FuzzyVariableListViewModel
    {
        public FuzzyVariableListVm()
            : base(new NumericVariableListViewModel())
        {
            var fuzzyVariable = new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>()) { Identifier = "Fuzzy Variable 1" };
            Items.Add(fuzzyVariable);
            SelectedItem = fuzzyVariable;
        }
    }
}
