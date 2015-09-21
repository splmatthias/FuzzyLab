using System.Collections.Generic;

namespace fuzzyController.inference.valueMerger.strategies
{
    public interface IMergingStrategy
    {
        double Merge(IList<double> values);
    }
}
