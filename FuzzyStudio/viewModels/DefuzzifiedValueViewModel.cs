using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuzzyStudio.viewModels
{
    public class DefuzzifiedValueViewModel : ViewModel
    {
        public string FuzzyVariable
        {
            get { return _fuzzyVariable; }
            set { SetProperty(ref _fuzzyVariable, value, "FuzzyVariable"); }
        }

        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, "Value"); }
        }

        private string _fuzzyVariable;
        private double _value;
    }
}
