using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.inference.valueMerger.strategies
{
    public class AverageValueStrategy: IMergingStrategy
    {
        public double Merge(IList<double> values)
        {
            return values.Average();
        }

        public override string ToString()
        {
            return "Average Value";
        }
    }
}
