namespace fuzzyStudio.viewModels
{
    public class NumericValueViewModel : NumericVariableViewModel
    {
        public double? Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, "Value"); }
        }

        private double? _value;
    }
}
