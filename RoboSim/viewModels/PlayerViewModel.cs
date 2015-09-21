using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSim.viewModels
{
    public class PlayerViewModel: FieldObjectViewModel
    {
        public int No { get; private set; }
        public string Team { get; private set; }


        public PlayerViewModel(int no, string team)
            : base(0, 0, "Available", "Disabled", "Penalized", "NotPlaying")
        {
            No = no;
            Team = team;
            States.SelectedItem = "Available";
        }

        public PlayerViewModel(int no, string team, double x, double y)
            : this(no, team)
        {
            X = x;
            Y = y;
        }
    }
}
