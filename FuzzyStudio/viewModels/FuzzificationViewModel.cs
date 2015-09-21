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
    public class FuzzificationViewModel :ViewModel
    {
        public FuzzificationViewModel(ObservableCollection<FuzzyVariableViewModel> variableSource )
        {
            if (variableSource == null)
                throw new ArgumentNullException("variableSource");

            variableSource.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (var item in args.NewItems)
                    {
                        AvailableVariables.Add(item as FuzzyVariableViewModel);
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (var item in args.OldItems)
                    {
                        AvailableVariables.Remove(item as FuzzyVariableViewModel);
                        InitialScope.Remove(item as FuzzyVariableViewModel);
                    }
                }
            };

            AvailableVariables = new ObservableCollection<FuzzyVariableViewModel>();
            InitialScope = new ObservableCollection<FuzzyVariableViewModel>();

            AddToScope = new DelegateCommand(
                p =>
                {
                    AvailableVariables.Remove(p as FuzzyVariableViewModel);
                    InitialScope.Add(p as FuzzyVariableViewModel);
                });

            RemoveFromScope = new DelegateCommand(
                p =>
                {
                    InitialScope.Remove(p as FuzzyVariableViewModel);
                    AvailableVariables.Add(p as FuzzyVariableViewModel);
                });
        }

        public ObservableCollection<FuzzyVariableViewModel> AvailableVariables { get; private set; }

        public ObservableCollection<FuzzyVariableViewModel> InitialScope { get; private set; }

        public ICommand AddToScope { get; private set; }

        public ICommand RemoveFromScope { get; private set; }
    }
}
