using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Windows.Input;

namespace fuzzyStudio.viewModels
{
    public class IterationViewModel : ViewModel
    {
        public IterationViewModel(int level, ObservableCollection<FuzzyVariableViewModel> inputScope,
            ObservableCollection<FuzzyVariableViewModel> allVariables)
        {
            _level = level;
            InputScope = inputScope;
            OutputScope = new ObservableCollection<FuzzyVariableViewModel>(inputScope);

            inputScope.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (var item in args.NewItems)
                    {
                        OutputScope.Add(item as FuzzyVariableViewModel);
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (var item in args.OldItems)
                    {
                        OutputScope.Remove(item as FuzzyVariableViewModel);
                    }
                }
            };
            Rules = new ObservableCollection<RuleViewModel>();
            Rules.CollectionChanged += onRulesCollectionChanged;

            AddRule = new DelegateCommand(p => Rules.Add(new RuleViewModel(inputScope, allVariables)));

            RemoveRule = new DelegateCommand(p => Rules.Remove(p as RuleViewModel));
        }

        private void onRulesCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (var item in args.NewItems)
                {
                    var vm = item as RuleViewModel;
                    if (vm != null)
                    {
                        vm.Conclusion.PropertyChanged += (o, eventArgs) =>
                        {
                            if (!InputScope.Contains(vm.Conclusion.SelectedVariable) && !OutputScope.Contains(vm.Conclusion.SelectedVariable))
                            {
                                OutputScope.Add(vm.Conclusion.SelectedVariable);
                            }
                        };
                        vm.Conclusion.PropertyChanging += (o, eventArgs) =>
                        {
                            if (!InputScope.Contains(vm.Conclusion.SelectedVariable))
                            {
                                OutputScope.Remove(vm.Conclusion.SelectedVariable);
                            }
                        };
                    }
                }
            }
        }

        public void RecalculateOutputScope()
        {
            foreach (var vm in Rules)
            {
                if (!InputScope.Contains(vm.Conclusion.SelectedVariable) && !OutputScope.Contains(vm.Conclusion.SelectedVariable))
                {
                    OutputScope.Add(vm.Conclusion.SelectedVariable);
                }
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                if (SetProperty(ref _level, value, "Level"))
                {
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Name { get { return "Level " + _level; } }

        public ObservableCollection<FuzzyVariableViewModel> InputScope { get; private set; }

        public ObservableCollection<FuzzyVariableViewModel> OutputScope { get; private set; }

        public ObservableCollection<RuleViewModel> Rules { get; private set; }

        public ICommand AddRule { get; private set; }

        public ICommand RemoveRule { get; private set; }

        private int _level;
    }
}