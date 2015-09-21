using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoboSim.viewModels;

namespace RoboSim.designData
{
    public class SituationVm : SituationViewModel
    {
        public SituationVm()
        {
            MinSituationValue = -5;
            MaxSituationValue = 5;
        }
    }
}
