using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.inference.valueMerger.strategies
{
    public class MinValueStrategy: IMergingStrategy
    {
        public double Merge(IList<double> values)
        {
            return values.Min();
        }

        public override string ToString()
        {
            return "Minimum Value";
        }
    }
}
