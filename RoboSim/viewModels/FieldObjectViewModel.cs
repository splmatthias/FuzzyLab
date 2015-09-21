using System;
using WpfBase.viewModels;

namespace RoboSim.viewModels
{
    public class FieldObjectViewModel : ViewModel
    {
        private double? _x;
        private double? _y;

        public ListViewModel<string> States { get; private set; }

        public double? X
        {
            get { return _x; }
            set
            {
                if (SetProperty(ref _x, value, "X"))
                {
                    if (OnPositionChanged != null)
                        OnPositionChanged();
                }
            }
        }

        public double? Y
        {
            get { return _y; }
            set
            {
                if (SetProperty(ref _y, value, "Y"))
                {
                    if (OnPositionChanged != null)
                        OnPositionChanged();
                }
            }
        }

        public FieldObjectViewModel(double x, double y, params string[] states)
        {
            _x = x;
            _y = y;
            States = new ListViewModel<string>(states);
        }

        public event Action OnPositionChanged;
    }
}