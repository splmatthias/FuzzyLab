using System.Collections.Generic;
using System.Collections.ObjectModel;
using fuzzyStudio.views;

namespace fuzzyStudio.viewModels
{
    public class ListViewModel<T, TV> : ViewModel<TV> where TV: class, IView where T: class
    {
        public ListViewModel(IEnumerable<T> items, T selectedItem = null)
            : base(null)
        {
            Items = new ObservableCollection<T>(items);
            SelectedItem = selectedItem;
        }

        public ObservableCollection<T> Items { get; set; }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value, "SelectedItem"); }
        }

        private T _selectedItem;
    }
}
