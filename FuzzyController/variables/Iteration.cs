using fuzzyController.expressions;
using System.Collections.Generic;

namespace fuzzyController.variables
{
    public class Iteration
    {
        public Iteration(IEnumerable<FuzzyImplication> implications)
        {
            Implications = new List<FuzzyImplication>(implications);
        }

        public readonly IList<FuzzyImplication> Implications;
    }
}
