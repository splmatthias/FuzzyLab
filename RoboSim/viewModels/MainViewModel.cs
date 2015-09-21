using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RoboSim.persistency;
using WpfBase.commands;
using WpfBase.viewModels;

namespace RoboSim.viewModels
{
    public class MainViewModel : ListViewModel<SituationViewModel>
    {
        private readonly Action<Dictionary<string, double>> _sendValues;
        private ViewService _viewService = new ViewService();
        

        public MainViewModel(Action<Dictionary<string, double>> sendValues = null)
        {
            _sendValues = sendValues;
            if (sendValues != null)
            {
                PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "SelectedItem")
                    {
                        if (SelectedItem != null)
                        {
                            var values = getValues();
                            sendValues(values);
                        }
                    }
                };
            }

            Items.Add(createNewSituation());
            SelectedItem = Items[0];

            CreateNewProject =
                new DelegateCommand(
                    () =>
                    {
                        Items.Clear();
                        var situation = createNewSituation();
                        Items.Add(situation);
                        SelectedItem = situation;
                    });

            OpenProject = new DelegateCommand(() =>
            {
                var file = _viewService.OpenFileDialog();

                if (string.IsNullOrEmpty(file))
                    return;

                Items.Clear();
                try
                {
                    var reader = new ConfigReaderWriter();
                    foreach(var situation in reader.ReadFromFile(file))
                    {
                        if (_sendValues != null)
                        {
                            situation.OnSituationChanged += () =>
                            {
                                var values = getValues();
                                _sendValues(values);
                            };
                        }
                        Items.Add(situation);
                    }
                    SelectedItem = Items[0];
                }
                catch (Exception)
                {
                    _viewService.ShowError("Could not load project file.");
                }
            });

            SaveProject = new DelegateCommand(() =>
            {
                var file = _viewService.SaveFileDialog();

                if (string.IsNullOrEmpty(file))
                    return;

                try
                {
                    var reader = new ConfigReaderWriter();
                    reader.WriteToFile(Items, file);
                }
                catch (Exception)
                {
                    _viewService.ShowError("Could not save project file.");
                }
            });

            AddSituation = new DelegateCommand(() =>
            {
                var situation = createNewSituation();
                Items.Add(situation);
                SelectedItem = situation;
            });

            RemoveSituation = new DelegateCommand(p =>
            {
                var situation = p as SituationViewModel;
                int index = Items.IndexOf(situation);
                
                Items.Remove(situation);
            });

            Randomize = new DelegateCommand(p =>
                {
                    if (SelectedItem != null)
                    {
                        foreach(var player in SelectedItem.Opponents.Where(o => o.No > 1))
                            setRandomPoint(player);
                        foreach (var player in SelectedItem.OwnTeam.Where(o => o.No > 1))
                            setRandomPoint(player);

                        setRandomPoint(SelectedItem.Ball);

                    }
                }, 
                p => SelectedItem != null
            );
        }

        private SituationViewModel createNewSituation()
        {
            var situation = new SituationViewModel();
            if(_sendValues != null)
            {
                situation.OnSituationChanged += () =>
                {
                    var values = getValues();
                    _sendValues(values);
                };
            }
            return situation;
        }

        public void UpdateResult(double minValue, double maxValue, double value)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedItem != null )
                {
                    SelectedItem.MinSituationValue = minValue;
                    SelectedItem.MaxSituationValue = maxValue;
                    SelectedItem.SituationValue = value;
                }
            });
        }

        private Dictionary<string, double> getValues()
        {
            var result = new Dictionary<string, double>();

            Point? ball = null;
            if (SelectedItem.Ball.X.HasValue && SelectedItem.Ball.Y.HasValue)
            {
                ball = new Point(SelectedItem.Ball.X.Value, SelectedItem.Ball.Y.Value);
            }
            Point? teamCenter = getCenter(SelectedItem.OwnTeam);
            Point? opponentCenter = getCenter(SelectedItem.Opponents);

            var teamGoal = new Point(-4500, 0);
            var opponentGoal = new Point(4500, 0);

            if (ball.HasValue)
                result["Position_Ball"] = ball.Value.X;

            if (teamCenter.HasValue)
                result["Team_Center_Position"] = teamCenter.Value.X;

            if (opponentCenter.HasValue)
                result["Opponent_Center_Position"] = opponentCenter.Value.X;

            fillMinDistance("Team_Min_Distance_To_Ball", result, ball, SelectedItem.OwnTeam);
            fillMinDistance("Opponent_Min_Distance_To_Ball", result, ball, SelectedItem.Opponents);

            fillMedianDistance("Team_Center_Distance_To_Ball", result, ball, SelectedItem.OwnTeam);
            fillMedianDistance("Opponent_Center_Distance_To_Ball", result, ball, SelectedItem.Opponents);
            //fillDistance("Team_Center_Distance_To_Ball", result, ball, teamCenter);
            //fillDistance("Opponent_Center_Distance_To_Ball", result, ball, opponentCenter);

            fillDistance("Ball_Distance_To_Team_Goal", result, ball, teamGoal);
            fillDistance("Ball_Distance_To_Opponent_Goal", result, ball, opponentGoal);

            fillMedianDistance("Team_Center_Distance_To_Team_Goal", result, ball, SelectedItem.OwnTeam);
            fillMedianDistance("Team_Center_Distance_To_Opponent_Goal", result, ball, SelectedItem.Opponents);
            //fillDistance("Team_Center_Distance_To_Team_Goal", result, teamCenter, teamGoal);
            //fillDistance("Team_Center_Distance_To_Opponent_Goal", result, teamCenter, opponentGoal);

            fillMedianDistance("Opponent_Center_Distance_To_Team_Goal", result, ball, SelectedItem.OwnTeam);
            fillMedianDistance("Opponent_Center_Distance_To_Opponent_Goal", result, ball, SelectedItem.Opponents);
            //fillDistance("Opponent_Center_Distance_To_Team_Goal", result, opponentCenter, teamGoal);
            //fillDistance("Opponent_Center_Distance_To_Opponent_Goal", result, opponentCenter, opponentGoal);

            fillMinDistance("Team_Min_Distance_To_Team_Goal", result, teamGoal, SelectedItem.OwnTeam);
            fillMinDistance("Opponent_Min_Distance_To_Team_Goal", result, teamGoal, SelectedItem.Opponents);

            fillMinDistance("Team_Min_Distance_To_Opponent_Goal", result, opponentGoal, SelectedItem.OwnTeam);
            fillMinDistance("Opponent_Min_Distance_To_Opponent_Goal", result, opponentGoal, SelectedItem.Opponents);

            
            return result;
        }

        private void fillMedianDistance(string key, Dictionary<string, double> dict, Point? point1, ObservableCollection<PlayerViewModel> players)
        {
            var distances = new List<double>();
            if (point1.HasValue)
            {
                foreach (var player in players)
                {
                    if (player.No != 1 && player.X.HasValue && player.Y.HasValue)
                    {
                        var distance = (point1.Value.X - player.X.Value) * (point1.Value.X - player.X.Value)
                                     + (point1.Value.Y - player.Y.Value) * (point1.Value.Y - player.Y.Value);
                        distances.Add(Math.Sqrt(distance));
                    }
                }
            }
            distances.Sort();
            dict[key] = distances[distances.Count/2];
        }


        private void fillMinDistance(string key, Dictionary<string, double> dict, Point? point1, IEnumerable<PlayerViewModel> players)
        {
            var minDistance = double.MaxValue;
            if (point1.HasValue)
            {
                foreach (var player in players)
                {
                    if (player.No != 1 && player.X.HasValue && player.Y.HasValue)
                    {
                        var distance = (point1.Value.X - player.X.Value)*(point1.Value.X - player.X.Value)
                                     + (point1.Value.Y - player.Y.Value)*(point1.Value.Y - player.Y.Value);
                        if (distance < minDistance)
                            minDistance = distance;
                    }
                }
            }
            dict[key] = Math.Sqrt(minDistance);
        }

        private void fillDistance(string key, Dictionary<string, double> dict, Point? point1, Point? point2)
        {
            if (point1.HasValue && point2.HasValue)
            {
                dict[key] =
                    Math.Sqrt((point1.Value.X - point2.Value.X) * (point1.Value.X - point2.Value.X)
                              + (point1.Value.Y - point2.Value.Y) * (point1.Value.Y - point2.Value.Y));
            }
        }

        private Point? getCenter(IEnumerable<PlayerViewModel> ownTeam)
        {
            double centerX = 0;
            double centerY = 0;
            int count = 0;
            foreach (var player in ownTeam)
            {
                if (player.No != 1 && player.X.HasValue && player.Y.HasValue)
                {
                    count++;
                    centerX += player.X.Value;
                    centerY += player.Y.Value;
                }
            }
            if (count == 0)
                return null;
            return new Point(centerX/count, centerY/count);
        }

        private readonly Random random = new Random();

        private void setRandomPoint(FieldObjectViewModel obj)
        {
            var point = new Point(random.Next(-4500, 4500), random.Next(-3000, 3000));
            obj.X = point.X;
            obj.Y = point.Y;
        }

        public ICommand CreateNewProject { get; private set; }

        public ICommand OpenProject { get; private set; }

        public ICommand SaveProject { get; private set; }

        public ICommand AddSituation { get; private set; }

        public ICommand RemoveSituation { get; private set; }

        public ICommand Randomize { get; private set; }
    }
}
