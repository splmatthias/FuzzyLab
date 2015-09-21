using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBase.viewModels;

namespace fuzzyStudio.viewModels
{
    public class IterationListViewModel : ListViewModel<IterationViewModel>
    {
        public IterationListViewModel(ObservableCollection<FuzzyVariableViewModel> initialScope, ObservableCollection<FuzzyVariableViewModel> allVariables)
        {
            Items.CollectionChanged += Items_CollectionChanged;
            AddIteration = new DelegateCommand(
                p =>
                {
                    if (_currentLevel == 0)
                    {
                        Items.Add(new IterationViewModel(++_currentLevel, initialScope, allVariables));
                    }
                    else
                    {
                        var lastIteration = Items[_currentLevel - 1];
                        Items.Add(new IterationViewModel(++_currentLevel, lastIteration.OutputScope, allVariables));
                    }
                    CommandManager.InvalidateRequerySuggested();
                });
            RemoveIteration = new DelegateCommand(p =>
            {
                if (_currentLevel > 0)
                {
                    Items.RemoveAt(--_currentLevel);
                }
            });
        }

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
                foreach (var item in e.NewItems)
                    _currentLevel ++;
        }

        public ICommand AddIteration { get; private set; }

        public ICommand RemoveIteration { get; private set; }

        private int _currentLevel = 0;
    }
}
