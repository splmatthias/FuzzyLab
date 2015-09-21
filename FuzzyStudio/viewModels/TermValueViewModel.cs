namespace fuzzyStudio.viewModels
{
    public class TermValueViewModel: ViewModel
    {
        public string Term
        {
            get { return _term; }
            set { SetProperty(ref _term, value, "Term"); }
        }
        
        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, "Value"); }
        }

        private string _term;
        private double _value;
    }
}
