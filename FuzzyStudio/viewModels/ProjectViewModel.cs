using System;
using System.Windows;
using fuzzyController;
using fuzzyController.defuzzifier;
using fuzzyController.expressions;
using fuzzyController.fuzzifier;
using fuzzyController.inference;
using fuzzyController.inference.valueMerger;
using fuzzyController.variables;
using fuzzyStudio.services;
using fuzzyStudio.views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace fuzzyStudio.viewModels
{
    public class ProjectViewModel : ViewModel<IView>
    {
        public ProjectViewModel(string name, FuzzyConfiguration fuzzyConfiguration)
            : this(name)
        {
            foreach (var numVar in fuzzyConfiguration.NumericVariables)
            {
                NumericVariables.Items.Add(new NumericVariableViewModel(numVar.Identifier)
                {
                    MinValue = Math.Abs(double.MinValue - numVar.MinValue) < 0.000001 ? (double?)null : numVar.MinValue,
                    MaxValue = Math.Abs(double.MaxValue - numVar.MaxValue) < 0.000001 ? (double?)null : numVar.MaxValue
                });
            }

            foreach (var fuzzyVar in fuzzyConfiguration.FuzzyVariables)
            {
                var fuzzyVariableViewModel = new FuzzyVariableViewModel(NumericVariables.Items)
                {
                    Identifier = fuzzyVar.Identifier,
                        NumericVariable = fuzzyVar.NumericVariable != null ? NumericVariables.Items.FirstOrDefault(v => v.Identifier == fuzzyVar.NumericVariable.Identifier) : null
                };
                var termViewModels = fuzzyVar.FuzzyTerms.Select(t =>
                {
                    var oneValues = new List<double>();
                    var zeroValues = new List<double>();
                    foreach (var term in t.MembershipFunction)
                    {
                        if (Math.Abs(term.Value - 1) < 0.0000000000001)
                            oneValues.Add(term.Key);
                        else if (Math.Abs(term.Value) < 0.0000000000001)
                            zeroValues.Add(term.Key);
                    }
                    return new TermViewModel(t.Term)
                    {
                        OneValues = string.Join(";", oneValues),
                        ZeroValues = string.Join(";", zeroValues)
                    };
                });
                foreach (var term in termViewModels)
                    fuzzyVariableViewModel.Terms.Add(term);

                FuzzyVariables.Items.Add(fuzzyVariableViewModel);
            }

            foreach(var variable in FuzzyVariables.Items.Where(varVm => fuzzyConfiguration.Fuzzification.FirstOrDefault(v => v.Identifier == varVm.Identifier) != null))
                Fuzzification.InitialScope.Add(variable);
            
            var inputScope = Fuzzification.InitialScope;
            for (int index = 0; index < fuzzyConfiguration.Iterations.Count; index++)
            {
                var iteration = fuzzyConfiguration.Iterations[index];
                var iterationVm = new IterationViewModel(index + 1, inputScope, FuzzyVariables.Items);
                foreach (var rule in iteration.Implications)
                {
                    var ruleVm = new RuleViewModel(inputScope, FuzzyVariables.Items);
                    fillExpressionViewModel(ruleVm.Premise[0], rule.Premise);
                    fillExpressionViewModel(ruleVm.Conclusion, rule.Conclusion); 
                    iterationVm.Rules.Add(ruleVm);
                }
                iterationVm.RecalculateOutputScope();
                Iterations.Items.Add(iterationVm);
                inputScope = iterationVm.OutputScope;
            }

            foreach (var variable in FuzzyVariables.Items.Where(varVm => fuzzyConfiguration.Defuzzification.FirstOrDefault(v => v.Identifier == varVm.Identifier) != null))
                Defuzzification.VariablesToDefuzzify.Add(variable);
        }

        private void fillExpressionViewModel(ExpressionViewModel viewModel, IFuzzyExpression expression)
        {
            if (expression is NotExpression)
            {
                viewModel.SelectedType = "NOT";
                fillExpressionViewModel(viewModel.SubExpressions[0] as ExpressionViewModel, (expression as NotExpression).Expression);
            }
            else if (expression is AndExpression)
            {
                viewModel.SelectedType = "AND";
                fillExpressionViewModel(viewModel.SubExpressions[0] as ExpressionViewModel,
                    (expression as AndExpression).LeftExpression);
                fillExpressionViewModel(viewModel.SubExpressions[1] as ExpressionViewModel,
                    (expression as AndExpression).RightExpression);
            }
            else if (expression is OrExpression)
            {
                viewModel.SelectedType = "OR";
                fillExpressionViewModel(viewModel.SubExpressions[0] as ExpressionViewModel,
                    (expression as OrExpression).LeftExpression);
                fillExpressionViewModel(viewModel.SubExpressions[1] as ExpressionViewModel,
                    (expression as OrExpression).RightExpression);
            }
            else if (expression is ValueExpression)
            {
                viewModel.SelectedType = "VALUE";
                fillExpressionViewModel(viewModel.SubExpressions[0] as ValueExpressionViewModel, expression as ValueExpression);
            }
        }

        private void fillExpressionViewModel(ValueExpressionViewModel viewModel, ValueExpression expression)
        {
            var variable = viewModel.Variables.FirstOrDefault(v => v.Identifier == expression.Variable.Identifier);
            viewModel.SelectedVariable = variable;

            var value = viewModel.Values.FirstOrDefault(v => v.Term == expression.Value.Term);
            viewModel.SelectedValue = value;
        }

        public ProjectViewModel(string name)
            : base(null)
        {
            _name = name;

            ControllerConfiguration = new ControllerConfigViewModel(new ConfigurationService(), null);
            NumericVariables = new NumericVariableListViewModel();
            FuzzyVariables = new FuzzyVariableListViewModel(NumericVariables);
            Fuzzification = new FuzzificationViewModel(FuzzyVariables.Items);
            Defuzzification = new DefuzzificationViewModel(FuzzyVariables.Items);
            Iterations = new IterationListViewModel(Fuzzification.InitialScope, FuzzyVariables.Items);

            Iterations.Items.CollectionChanged += (sender, args) =>
            {
                if (Iterations.Items.Any())
                {
                    Defuzzification.VariableSource = Iterations.Items[Iterations.Items.Count-1].OutputScope;
                }
                else
                {
                    Defuzzification.VariableSource = null;
                }
            };
            Evaluations = new ObservableCollection<EvaluationViewModel>();
        }

        public object CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value, "CurrentView"); }
        }

        public FuzzyConfiguration GetFuzzyConfiguration()
        {
            return getFuzzyConfiguration();
        }

        public ControllerConfigViewModel ControllerConfiguration { get; private set; }

        public NumericVariableListViewModel NumericVariables { get; private set; }

        public FuzzyVariableListViewModel FuzzyVariables { get; private set; }

        public FuzzificationViewModel Fuzzification { get; private set; }

        public DefuzzificationViewModel Defuzzification { get; private set; }

        public IterationListViewModel Iterations { get; private set; }

        public ObservableCollection<EvaluationViewModel> Evaluations { get; private set; }

        public bool HasChanged
        {
            get { return _hasChanged; }
            set { SetProperty(ref _hasChanged, value, "HasChanged"); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }
        
        public ICommand StartSingleEvaluation
        {
            get
            {
                return _startSingleEvaluation ??
                       (_startSingleEvaluation = new DelegateCommand(p =>
                       {
                           var fuzzyConfig = getFuzzyConfiguration();
                           var controllerConfig = getControllerConfiguration(fuzzyConfig);

                           Evaluations.Add(new EvaluationViewModel(Fuzzification.InitialScope, fuzzyConfig, controllerConfig));
                       }));
            }
        }

        private ControllerConfiguration getControllerConfiguration(FuzzyConfiguration fuzzyConfig)
        {
            return new ControllerConfiguration(
                new Fuzzifier(fuzzyConfig.FuzzyVariables.ToArray()),
                new RuleEvaluation( ControllerConfiguration.EvaluationStrategies.SelectedItem, new FuzzyValueMerger(ControllerConfiguration.MergingStrategies.SelectedItem)),
                new FuzzyValueMerger(ControllerConfiguration.MergingStrategies.SelectedItem),
                new Defuzzifier(ControllerConfiguration.ScaleStrategies.SelectedItem,
                    ControllerConfiguration.MergeStrategies.SelectedItem,
                    ControllerConfiguration.DefuzzifyStrategies.SelectedItem));
        }

        private FuzzyConfiguration getFuzzyConfiguration()
        {
            var fuzzyVariables = getFuzzyVariables();
            var fuzzification = new List<FuzzyVariable>();
            var defuzzification = new List<FuzzyVariable>();

            foreach (var fuzzyVar in Fuzzification.InitialScope)
                fuzzification.Add(createFuzzyVariable(fuzzyVar));

            foreach (var fuzzyVar in Defuzzification.VariablesToDefuzzify)
                defuzzification.Add(createFuzzyVariable(fuzzyVar));

            return new FuzzyConfiguration(
                        getNumericVariables(), 
                        fuzzyVariables,
                        fuzzification,
                        getRules(fuzzyVariables),
                        defuzzification);
        }

        private IList<Iteration> getRules(List<FuzzyVariable> fuzzyVariables)
        {
            return Iterations.Items.Select(ivm =>
            {
                return new Iteration(ivm.Rules.Select(rvm =>
                {
                    var expression = convertToExpression(rvm.Premise[0], fuzzyVariables);
                    var conclusion = convertToExpression(rvm.Conclusion, fuzzyVariables);
                    return new FuzzyImplication(expression, conclusion);
                }));
            }).ToList();
        }

        private ValueExpression convertToExpression(ValueExpressionViewModel vm, List<FuzzyVariable> fuzzyVariables)
        {
            var fuzzyVar = fuzzyVariables.Find(v => v.Identifier == vm.SelectedVariable.Identifier);
            var fuzzyTerm = fuzzyVar.FuzzyTerms.FirstOrDefault(v => v.Term == vm.SelectedValue.Term);
            return new ValueExpression(fuzzyVar, fuzzyTerm);
        }

        private IFuzzyExpression convertToExpression(ExpressionViewModel vm, List<FuzzyVariable> fuzzyVariables)
        {
            if (vm.SelectedType == "NOT")
            {
                return new NotExpression(convertToExpression(vm.SubExpressions[0] as ExpressionViewModel, fuzzyVariables));
            }
            if (vm.SelectedType == "OR")
            {
                return new OrExpression(
                                convertToExpression(vm.SubExpressions[0] as ExpressionViewModel, fuzzyVariables),
                                convertToExpression(vm.SubExpressions[1] as ExpressionViewModel, fuzzyVariables));
            }
            if (vm.SelectedType == "AND")
            {
                return new AndExpression(
                                convertToExpression(vm.SubExpressions[0] as ExpressionViewModel, fuzzyVariables),
                                convertToExpression(vm.SubExpressions[1] as ExpressionViewModel, fuzzyVariables));
            }
            if (vm.SelectedType == "VALUE")
            {
                return convertToExpression(vm.SubExpressions[0] as ValueExpressionViewModel, fuzzyVariables);
            }
            return null;
        }

        private IList<NumericVariable> getNumericVariables()
        {
            return NumericVariables.Items.Select(createNumericVariable).ToList();
        }

        private NumericVariable createNumericVariable(NumericVariableViewModel vm)
        {
            return new NumericVariable(vm.Identifier, vm.MinValue, vm.MaxValue);
        }

        private List<FuzzyVariable> getFuzzyVariables()
        {
            return FuzzyVariables.Items.Select(createFuzzyVariable).ToList();
        }

        private FuzzyVariable createFuzzyVariable(FuzzyVariableViewModel vm)
        {
            var numVar = vm.NumericVariable != null ? createNumericVariable(vm.NumericVariable) : null;
            return new FuzzyVariable(vm.Identifier, numVar, vm.Terms.Select(createFuzzyTerm).ToArray());
        }

        private FuzzyTerm createFuzzyTerm(TermViewModel vm)
        {
            var msf = new MembershipFunction();
            foreach (var point in vm.MsfPoints)
            {
                msf.Add(point.X, point.Y);
            }
            return new FuzzyTerm(vm.Term, msf);
        }

        private string _name;
        private object _currentView;
        private bool _hasChanged;
        private DelegateCommand _startSingleEvaluation;
    }
}
