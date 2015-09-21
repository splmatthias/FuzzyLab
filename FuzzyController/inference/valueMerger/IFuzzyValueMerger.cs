using System.Collections.Generic;
using fuzzyController.variables;

namespace fuzzyController.inference.valueMerger
{
    public interface IFuzzyValueMerger
    {
        IList<FuzzyValue> Apply(IEnumerable<FuzzyValue> value);
    }
}
