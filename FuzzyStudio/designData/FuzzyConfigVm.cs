using System.Windows;
using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class FuzzyConfigVm : NumericVariableListViewModel
    {
        public FuzzyConfigVm()
        {
            Items.Add(new NumericVariableViewModel { Identifier = "VARIABLE_1", MaxValue = 42 });
            Items.Add(new NumericVariableViewModel { Identifier = "VARIABLE_2", MinValue = -42, MaxValue = 42 });

            //FuzzyVariables.Add(new FuzzyVariableViewModel{Identifier = "Fuzzy Variable 1"});
            //var term = new TermViewModel();
            //term.MsfPoints.Add(new Point(0, 0));
            //term.MsfPoints.Add(new Point(10, 1));
            //term.MsfPoints.Add(new Point(20, 1));
            //term.MsfPoints.Add(new Point(30, 0));
            //FuzzyVariables[0].Terms.Add(term);

            //FuzzyRules.Add(new FuzzyRuleViewModel {Identifier = "Fuzzy Rule 1"});
        }
    }
}
