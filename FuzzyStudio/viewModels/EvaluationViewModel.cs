using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using fuzzyController;
using fuzzyController.variables;

namespace fuzzyStudio.viewModels
{
    public class EvaluationViewModel : ViewModel
    {
        public EvaluationViewModel(IEnumerable<FuzzyVariableViewModel> input, FuzzyConfiguration fuzzyConfiguration, ControllerConfiguration controllerConfiguration)
            : this()
        {
            if (fuzzyConfiguration == null) 
                throw new ArgumentNullException("fuzzyConfiguration");
            if (controllerConfiguration == null) 
                throw new ArgumentNullException("controllerConfiguration");

            foreach (var inputVar in input.Select(item => item.NumericVariable))
            {
                var numVm = new NumericValueViewModel
                {
                    Identifier = inputVar.Identifier,
                    MinValue = inputVar.MinValue,
                    MaxValue = inputVar.MaxValue,
                    Value =  0
                };
                if (numVm.MinValue.HasValue && numVm.Value < numVm.MinValue.Value)
                    numVm.Value = numVm.MinValue;

                if (numVm.MaxValue.HasValue && numVm.Value > numVm.MaxValue.Value)
                    numVm.Value = numVm.MaxValue;

                NumericValues.Add(numVm);
            }

            _fuzzyController = new FuzzyController(fuzzyConfiguration, controllerConfiguration);
        }

        protected EvaluationViewModel()
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NumericValues = new ObservableCollection<NumericValueViewModel>();
            Scopes = new ObservableCollection<ScopeViewModel>();
            _useDirectInput = true;

            createCommands();
        }

        public string Date { get; private set; }

        public ObservableCollection<NumericValueViewModel> NumericValues { get; private set; }

        public ScopeViewModel Fuzzification
        {
            get { return _fuzzification; }
            set { SetProperty(ref _fuzzification,  value, "Fuzzification"); }
        }

        public ObservableCollection<ScopeViewModel> Scopes { get; private set; }

        public DefuzzificationResultViewModel Defuzzification
        {
            get { return _defuzzification; }
            set { SetProperty(ref _defuzzification, value, "Defuzzification"); }
        }
        
        public bool UseDirectInput
        {
            get { return _useDirectInput; }
            set { SetProperty(ref _useDirectInput, value, "UseDirectInput"); }
        }

        public ICommand OpenPlugin { get; private set; }

        public ICommand UpdateController { get; private set; } 

        private void createCommands()
        {
            OpenPlugin = new DelegateCommand(p =>
            {
                _inputModule = new RoboSim.MainWindow(onValuesSend);
                _inputModule.Show();
            });
            UpdateController = new DelegateCommand(p =>
            {
                var inputValues = NumericValues.Where(val => val.Value.HasValue).Select(val => new NumericValue(new NumericVariable(val.Identifier, val.MinValue, val.MaxValue), val.Value.Value)).ToArray();
                var result = _fuzzyController.Evaluate(inputValues);

                Fuzzification = createScopeViewModel(result.Fuzzification);

                Scopes.Clear();
                foreach (var scope in result.Iterations.Select(createScopeViewModel))
                {
                    Scopes.Add(scope);
                }

                Defuzzification = new DefuzzificationResultViewModel();
                foreach (var defuzzifiedValue in result.Defuzzification)
                {
                    Defuzzification.Items.Add(new DefuzzifiedValueViewModel{ FuzzyVariable = defuzzifiedValue.Variable.Identifier, Value =  defuzzifiedValue.Value});

                    if (_inputModule != null)
                    {
                        _inputModule.ViewModel.UpdateResult(
                            defuzzifiedValue.Variable.NumericVariable.MinValue,
                            defuzzifiedValue.Variable.NumericVariable.MaxValue,
                            defuzzifiedValue.Value
                        );
                    }
                }
            });
        }

        private static ScopeViewModel createScopeViewModel(Scope scope)
        {
            var valueVms = scope.Select(v =>
            {
                var valueVm = new FuzzyValueViewModel
                {
                    FuzzyVariable = v.AssociatedVariable.Identifier
                };
                foreach (var vm in v.Values.Where(i => i.Value > 0).Select(i => new TermValueViewModel {Term = i.Key.Term, Value = i.Value}))
                    valueVm.Values.Add(vm);

                return valueVm;
            });

            var scopeVm = new ScopeViewModel {Title = "Level "};
            foreach (var val in valueVms)
                scopeVm.Values.Add(val);
            return scopeVm;
        }

        private void onValuesSend(Dictionary<string, double> values)
        {
            foreach (var numValue in NumericValues)
            {
                double value;
                if (values.TryGetValue(numValue.Identifier, out value))
                    numValue.Value = value;
            }
            UpdateController.Execute(null);
        }

        private readonly FuzzyController _fuzzyController;
        private bool _useDirectInput;
        private ScopeViewModel _fuzzification;
        private DefuzzificationResultViewModel _defuzzification;
        private RoboSim.MainWindow _inputModule;
    }
}
