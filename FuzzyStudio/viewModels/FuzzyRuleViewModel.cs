namespace fuzzyStudio.viewModels
{
    public class FuzzyRuleViewModel : ViewModel
    {
        public string Identifier
        {
            get { return _identifier; }
            set { SetProperty(ref _identifier, value, "Identifier"); }
        }

        private string _identifier;
    }
}
