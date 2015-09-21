using System.Collections.Generic;
using fuzzyController.variables;

namespace fuzzyController.fuzzifier
{
    public interface IFuzzifier
    {
        IList<FuzzyValue> Apply(params NumericValue[] numericValues);
    }
}
