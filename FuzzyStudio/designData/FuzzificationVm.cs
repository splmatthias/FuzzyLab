using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyStudio.viewModels;
using fuzzyStudio.views;

namespace fuzzyStudio.designData
{
    public class FuzzificationVm : FuzzificationViewModel
    {
        public FuzzificationVm()
            : base(new ObservableCollection<FuzzyVariableViewModel>())
        {
            AvailableVariables.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 1"
            });
            AvailableVariables.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 3"
            });
            AvailableVariables.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 4"
            });
            AvailableVariables.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 5"
            });
            AvailableVariables.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 7"
            });

            InitialScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 2"
            });
            InitialScope.Add(new FuzzyVariableViewModel(new ObservableCollection<NumericVariableViewModel>())
            {
                Identifier = "Variable 6"
            });
        }
    }
}
