using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace fuzzyStudio.viewModels
{
    public class DefuzzificationViewModel: ViewModel
    {
        public DefuzzificationViewModel(ObservableCollection<FuzzyVariableViewModel> variableSource)
        {
            if (variableSource == null)
                throw new ArgumentNullException("variableSource");

            LastScope = new ObservableCollection<FuzzyVariableViewModel>();
            VariablesToDefuzzify = new ObservableCollection<FuzzyVariableViewModel>();


            PropertyChanging += (sender, args) =>
            {
                if (args.PropertyName == "VariableSource" && _variableSource != null)
                {
                    _variableSource.CollectionChanged -= onSourceCollectionChanged;
                }
            };

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "VariableSource" && _variableSource != null)
                {
                    _variableSource.CollectionChanged += onSourceCollectionChanged;
                }
            };

            VariableSource = variableSource;


            AddToDefuzzification = new DelegateCommand(
                p =>
                {
                    LastScope.Remove(p as FuzzyVariableViewModel);
                    VariablesToDefuzzify.Add(p as FuzzyVariableViewModel);
                });

            RemoveFromDefuzzification = new DelegateCommand(
                p =>
                {
                    VariablesToDefuzzify.Remove(p as FuzzyVariableViewModel);
                    LastScope.Add(p as FuzzyVariableViewModel);
                });
        }

        private void onSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (var item in args.NewItems)
                {
                    LastScope.Add(item as FuzzyVariableViewModel);
                }
            }

            if (args.OldItems != null)
            {
                foreach (var item in args.OldItems)
                {
                    LastScope.Remove(item as FuzzyVariableViewModel);
                    VariablesToDefuzzify.Remove(item as FuzzyVariableViewModel);
                }
            }
        }

        public ObservableCollection<FuzzyVariableViewModel> VariableSource
        {
            get { return _variableSource; }
            set
            {
                if (SetProperty(ref _variableSource, value, "VariableSource"))
                {
                    if (_variableSource != null)
                    {
                        var varToRemove = LastScope.Where(v => !_variableSource.Contains(v)).ToList();
                        var varToAdd = _variableSource.Where(v => !LastScope.Contains(v)).ToList();
                        foreach (var v in varToRemove)
                        {
                            LastScope.Remove(v);
                            VariablesToDefuzzify.Remove(v);
                        }
                        foreach (var v in varToAdd)
                        {
                            LastScope.Add(v);
                        }
                    }
                    else
                    {
                        LastScope.Clear();
                        VariablesToDefuzzify.Clear();
                    }
                }
            }
        }

        public ObservableCollection<FuzzyVariableViewModel> LastScope { get; private set; }

        public ObservableCollection<FuzzyVariableViewModel> VariablesToDefuzzify { get; private set; }

        public ICommand AddToDefuzzification { get; private set; }

        public ICommand RemoveFromDefuzzification { get; private set; }

        private ObservableCollection<FuzzyVariableViewModel> _variableSource;
    }
}
