using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class DefuzzificationVm : DefuzzificationViewModel
    {
        public DefuzzificationVm() : 
            base(new ObservableCollection<FuzzyVariableViewModel>())
        {

        }
    }
}
