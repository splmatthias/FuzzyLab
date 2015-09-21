using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.inference.valueMerger.strategies
{
    [Default]
    public class MaxValueStrategy : IMergingStrategy
    {
        public double Merge(IList<double> values)
        {
            return values.Max();
        }

        public override string ToString()
        {
            return "Maximum Value";
        }
    }
}
