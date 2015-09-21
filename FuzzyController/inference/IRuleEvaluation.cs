using fuzzyController.expressions;
using fuzzyController.variables;
using System.Collections.Generic;

namespace fuzzyController.inference
{
    public interface IRuleEvaluation
    {
        Scope Apply(Scope scope, IEnumerable<FuzzyImplication> implications);
    }
}
