using System.Collections.ObjectModel;
using System.Windows;

namespace fuzzyStudio.viewModels
{
    public class TermViewModel : ViewModel
    {
        public TermViewModel(string term)
        {
            Term = term;
            MsfPoints = new ObservableCollection<Point>();
        }

        public ObservableCollection<Point> MsfPoints { get; private set; }

        public string Term
        {
            get { return _term; }
            set { SetProperty(ref _term, value, "Term"); }
        }

        public string OneValues
        {
            get { return _oneValues; }
            set
            {
                if (SetProperty(ref _oneValues, value, "OneValues"))
                {
                    updateMsfPoint();
                }
            }
        }

        public string ZeroValues
        {
            get { return _zeroValues; }
            set
            {
                if (SetProperty(ref _zeroValues, value, "ZeroValues"))
                {
                    updateMsfPoint();
                }
            }
        }

        private void updateMsfPoint()
        {
            MsfPoints.Clear();

            addValuesToMsf(OneValues, 1);
            addValuesToMsf(ZeroValues, 0);
        }

        private void addValuesToMsf(string values, double yValue)
        {
            if (values != null)
            {
                var strValues = values.Split(';');
                foreach (var strValue in strValues)
                {
                    double value;
                    if (double.TryParse(strValue, out value))
                        MsfPoints.Add(new Point(value, yValue));
                }
            }
        }

        private string _term;
        private string _oneValues;
        private string _zeroValues;
    }
}
