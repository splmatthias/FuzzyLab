using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using fuzzyStudio.viewModels;

namespace FuzzyStudio.views
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var obj = e.NewValue as TreeViewItem;
            if (obj != null)
            {
                var dataContext = obj.DataContext;
                if (dataContext != null)
                {
                    var vm = DataContext as ProjectViewModel;
                    if(vm != null && vm != dataContext)
                        vm.CurrentView = dataContext;
                }
            }
            else if (e.NewValue is ViewModel)
            {
                var vm = DataContext as ProjectViewModel;
                if (vm != null)
                    vm.CurrentView = e.NewValue;
            }
        }
    }
}
