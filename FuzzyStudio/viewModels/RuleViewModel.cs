using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuzzyStudio.viewModels
{
    public class RuleViewModel : ViewModel
    {
        public RuleViewModel(ObservableCollection<FuzzyVariableViewModel> inputScope, ObservableCollection<FuzzyVariableViewModel> allVariables)
        {
            Premise = new ObservableCollection<ExpressionViewModel> { new ExpressionViewModel(inputScope) };
            Conclusion = new ValueExpressionViewModel(allVariables);
        }

        public ObservableCollection<ExpressionViewModel> Premise { get; private set; }

        public ValueExpressionViewModel Conclusion { get; private set; }
    }

    public class ExpressionViewModel : ViewModel
    {
        private readonly ObservableCollection<FuzzyVariableViewModel> _inputScope;

        public ExpressionViewModel(ObservableCollection<FuzzyVariableViewModel> inputScope)
        {
            _inputScope = inputScope;
            Types = new ObservableCollection<string> {"Select Expression", "NOT", "OR", "AND", "VALUE"};
            SubExpressions = new ObservableCollection<ViewModel>();
            _selectedType = Types[0];
        }

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (SetProperty(ref _selectedType, value, "SelectedType"))
                {
                    SubExpressions.Clear();
                    if (_selectedType == "NOT")
                    {
                        SubExpressions.Add(new ExpressionViewModel(_inputScope));
                    }
                    else if (_selectedType == "AND" || _selectedType == "OR")
                    {
                        SubExpressions.Add(new ExpressionViewModel(_inputScope));
                        SubExpressions.Add(new ExpressionViewModel(_inputScope));
                    }
                    else if (_selectedType == "VALUE")
                    {
                        SubExpressions.Add(new ValueExpressionViewModel(_inputScope));
                    }
                }
            }
        }

        public ObservableCollection<ViewModel> SubExpressions { get; private set; }

        public ObservableCollection<string> Types { get; private set; }

        private string _selectedType;
    }

    public class ValueExpressionViewModel : ViewModel
    {

        public ValueExpressionViewModel(ObservableCollection<FuzzyVariableViewModel> availableVariables)
        {
            Variables = availableVariables;
            Values = new ObservableCollection<TermViewModel>();
        }

        public FuzzyVariableViewModel SelectedVariable
        {
            get { return _selectedVariable; }
            set
            {
                if (SetProperty(ref _selectedVariable, value, "SelectedVariable"))
                {
                    Values.Clear();
                    if (SelectedVariable != null)
                    {
                        foreach(var term in SelectedVariable.Terms)
                            Values.Add(term);
                    }
                }
            }
        }

        public ObservableCollection<FuzzyVariableViewModel> Variables { get; private set; }

        public TermViewModel SelectedValue
        {
            get { return _selectedValue; }
            set { SetProperty(ref _selectedValue, value, "SelectedValue"); }
        }

        public ObservableCollection<TermViewModel> Values { get; private set; }

        private FuzzyVariableViewModel _selectedVariable;
        private TermViewModel _selectedValue;
    }
}
