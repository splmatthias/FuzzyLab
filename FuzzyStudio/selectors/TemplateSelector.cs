using System.Collections.ObjectModel;
using fuzzyStudio.viewModels;
using System.Windows;
using System.Windows.Controls;

namespace fuzzyStudio.selectors
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate ControllerConfig { get; set; }

        public DataTemplate FuzzyConfig { get; set; }

        public DataTemplate Evaluation { get; set; }

        public DataTemplate NumericVariables { get; set; }

        public DataTemplate IterationTemplate { get; set; }

        public DataTemplate FuzzyVariables { get; set; }

        public DataTemplate Fuzzification { get; set; }

        public DataTemplate Defuzzification { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ControllerConfigViewModel)
                return ControllerConfig;
            if (item is NumericVariableListViewModel)
                return NumericVariables;
            if (item is FuzzyVariableListViewModel)
                return FuzzyVariables;
            if (item is IterationViewModel)
                return IterationTemplate;
            if (item is FuzzificationViewModel)
                return Fuzzification;
            if (item is DefuzzificationViewModel)
                return Defuzzification;
            if (item is FuzzyConfigViewModel)
                return FuzzyConfig;
            if (item is EvaluationViewModel)
                return Evaluation;
            return base.SelectTemplate(item, container);
        }
    }
}
