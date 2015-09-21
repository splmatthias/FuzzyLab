using fuzzyController.variables;
using System.Collections.Generic;

namespace fuzzyController
{
    public class ControllerResult
    {
        public ControllerResult(Scope fuzzification, IEnumerable<Scope> iterations, IEnumerable<DefuzzifiedValue> defuzzification)
        {
            Fuzzification = fuzzification;
            Iterations = new List<Scope>(iterations);
            Defuzzification = new List<DefuzzifiedValue>(defuzzification);
        }

        public readonly Scope Fuzzification;

        public readonly IReadOnlyList<Scope> Iterations;

        public readonly IReadOnlyList<DefuzzifiedValue> Defuzzification;
    }
}
