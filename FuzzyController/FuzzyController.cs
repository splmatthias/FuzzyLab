using fuzzyController.defuzzifier;
using fuzzyController.fuzzifier;
using fuzzyController.inference;
using fuzzyController.variables;
using System.Collections.Generic;
using System.Linq;

namespace fuzzyController
{
    public class FuzzyController
    {
        public FuzzyController(FuzzyConfiguration fuzzyConfiguration, ControllerConfiguration controllerConfiguration)
        {
            _fuzzification = fuzzyConfiguration.Fuzzification;
            _iterations = fuzzyConfiguration.Iterations;
            _defuzzification = fuzzyConfiguration.Defuzzification;

            _fuzzifier = controllerConfiguration.Fuzzifier;
            _inference = controllerConfiguration.Inference;
            _defuzzifier = controllerConfiguration.Defuzzifier;
        }

        public ControllerResult Evaluate(params NumericValue[] input)
        {
            var initialScope = fuzzify(input);

            var iterationScopes = executeInference(initialScope);

            var defuzzifiedValues = defuzzify(iterationScopes[iterationScopes.Count - 1]);

            return new ControllerResult(initialScope, iterationScopes, defuzzifiedValues);
        }

        private Scope fuzzify(NumericValue[] input)
        {
            var values = _fuzzifier.Apply(input);
            return new Scope(values);
        }

        private List<Scope> executeInference(Scope scope)
        {
            var iterationScopes = new List<Scope>();
            var currentScope = scope;

            foreach (var iteration in _iterations)
            {
                currentScope = _inference.Apply(currentScope, iteration.Implications);
                iterationScopes.Add(currentScope);
            }

            return iterationScopes;
        }

        private IEnumerable<DefuzzifiedValue> defuzzify(IEnumerable<FuzzyValue> scope)
        {
            return scope
                .Where(i => _defuzzification.Contains(i.AssociatedVariable))
                .Select(_defuzzifier.Apply)
                .ToList();
        }

        private readonly IList<Iteration> _iterations; 
        private readonly IFuzzifier _fuzzifier;
        private readonly IRuleEvaluation _inference;
        private readonly IDefuzzifier _defuzzifier;
        private IList<FuzzyVariable> _fuzzification;
        private IList<FuzzyVariable> _defuzzification;
    }
}
