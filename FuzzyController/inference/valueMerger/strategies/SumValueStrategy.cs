using System;
using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.inference.valueMerger.strategies
{
    public class SumValueStrategy: IMergingStrategy
    {
        public double Merge(IList<double> values)
        {
            return values.Sum();
        }

        public override string ToString()
        {
            return "Sum Value";
        }
    }
}
