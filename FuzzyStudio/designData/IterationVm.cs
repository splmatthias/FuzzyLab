using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class IterationVm : IterationViewModel
    {
        public IterationVm()
            : base(1, new ObservableCollection<FuzzyVariableViewModel>(), new ObservableCollection<FuzzyVariableViewModel>())
        {
            InputScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>()) { Identifier = "InVar 1" });
            InputScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>()) { Identifier = "InVar 2" });
            InputScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>()) { Identifier = "InVar 3" });
            InputScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>()) { Identifier = "InVar 4" });
        }
    }
}
