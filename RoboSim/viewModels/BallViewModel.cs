using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBase.viewModels;

namespace RoboSim.viewModels
{
    public class BallViewModel : FieldObjectViewModel
    {
        public BallViewModel()
            : base(0, 0, "InGame", "NotInGame")
        {
            States.SelectedItem = "InGame";
        }
    }
}
