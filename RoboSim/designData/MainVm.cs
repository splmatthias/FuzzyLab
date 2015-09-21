using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoboSim.viewModels;

namespace RoboSim.designData
{
    public class MainVm : MainViewModel
    {
        public MainVm()
        {
            var situation = new SituationVm();
            Items.Add(new SituationVm());
            Items.Add(situation);
            SelectedItem = situation;
        }
    }
}
