using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase.viewModels;

namespace RoboSim.viewModels
{
    public class SituationViewModel : ViewModel
    {
        public ObservableCollection<PlayerViewModel> OwnTeam { get; private set; }
        public ObservableCollection<PlayerViewModel> Opponents{ get; private set; }

        public ObservableCollection<PlayerViewModel> AllObjects { get; private set; } 

        public BallViewModel Ball { get; private set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }

        private double _situationValue;
        private double _minSituationValue;
        private double _maxSituationValue;
        
        public double SituationValue
        {
            get { return _situationValue; }
            set { SetProperty(ref _situationValue, value, "SituationValue"); }
        }

        public double MinSituationValue
        {
            get { return _minSituationValue; }
            set { SetProperty(ref _minSituationValue, value, "MinSituationValue"); }
        }

        public double MaxSituationValue
        {
            get { return _maxSituationValue; }
            set { SetProperty(ref _maxSituationValue, value, "MaxSituationValue"); }
        }

        public SituationViewModel()
        {
            Name = "New Situation";
            SituationValue = 0;
            Ball = new BallViewModel();

            Ball.OnPositionChanged += onPositionChanged;

            OwnTeam = new ObservableCollection<PlayerViewModel>
            {
                new PlayerViewModel(1, "Own", -4500, 0),
                new PlayerViewModel(2, "Own", -2500, -1500),
                new PlayerViewModel(3, "Own", -2500,  -500),
                new PlayerViewModel(4, "Own", -2500,   500),
                new PlayerViewModel(5, "Own", -2500,  1500)
            };

            Opponents = new ObservableCollection<PlayerViewModel>
            {
                new PlayerViewModel(1, "Opponents", 4500, 0),
                new PlayerViewModel(2, "Opponents", 2500, -1500),
                new PlayerViewModel(3, "Opponents", 2500,  -500),
                new PlayerViewModel(4, "Opponents", 2500,   500),
                new PlayerViewModel(5, "Opponents", 2500,  1500)
            };

            AllObjects = new ObservableCollection<PlayerViewModel>();
            proccessObject(OwnTeam);
            proccessObject(Opponents);
        }

        private void proccessObject(IEnumerable<PlayerViewModel> objects)
        {
            foreach (var player in objects)
            {
                AllObjects.Add(player);
                player.OnPositionChanged += onPositionChanged;
            }
        }

        private void onPositionChanged()
        {
            if (OnSituationChanged != null)
                OnSituationChanged();
        }

        public event Action OnSituationChanged;
    }
}
