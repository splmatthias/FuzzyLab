namespace fuzzyStudio.viewModels
{
    public class NumericVariableViewModel : ViewModel
    {
        public NumericVariableViewModel(string identifier = null)
        {
            Identifier = identifier;
        }

        public string Identifier
        {
            get { return _identifier; }
            set { SetProperty(ref _identifier, value, "Identifier"); }
        }
        
        public double? MaxValue
        {
            get { return _maxValue; }
            set { SetProperty(ref _maxValue, value, "MaxValue"); }
        }

        public double? MinValue
        {
            get { return _minValue; }
            set { SetProperty(ref _minValue, value, "MinValue"); }
        }

        public override string ToString()
        {
            return _identifier;
        }

        private string _identifier;
        private double? _maxValue;
        private double? _minValue;
    }
}
