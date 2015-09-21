using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfBase.views;

namespace WpfBase.viewModels
{
    public class ListViewModel<T, TV> : ViewModel<TV> where TV: class, IView where T: class
    {
        public ObservableCollection<T> Items { get; set; }

        private T _selectedItem;
        public T SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value, "SelectedItem"); }
        }

        public ListViewModel(IList<T> items, T selectedItem = null) : base(null)
        {
            Items = new ObservableCollection<T>(items);
            SelectedItem = selectedItem;
        }
    }

    public class ListViewModel<T> : ViewModel
        where T : class
    {
        public ObservableCollection<T> Items { get; set; }

        private T _selectedItem;
        public T SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value, "SelectedItem"); }
        }

        public ListViewModel()
        {
            Items = new ObservableCollection<T>();
        } 

        public ListViewModel(IList<T> items, T selectedItem = null)
        {
            Items = new ObservableCollection<T>(items);
            SelectedItem = selectedItem;
        }
    }
}
