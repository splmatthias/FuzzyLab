using fuzzyController.variables;
using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.fuzzifier
{
    public class Fuzzifier : IFuzzifier
    {
        public Fuzzifier(params FuzzyVariable[] fuzzyVariables)
        {
            _fuzzyVariables = fuzzyVariables;
        }

        public IList<FuzzyValue> Apply(params NumericValue[] numericValues)
        {
            var result = new List<FuzzyValue>();

            foreach (var value in numericValues)
            {
                var fuzzyVariables = _fuzzyVariables.Where(v => v.NumericVariable != null && v.NumericVariable.Equals(value.Variable)).ToList();
                foreach (var fuzzyVariable in fuzzyVariables)
                {
                    var values = getMembershipValuesThatAreGreaterZero(fuzzyVariable, value);
                    if (values.Any())
                    {
                        result.Add(new FuzzyValue(fuzzyVariable, values));
                    }
                }
            }

            return result;
        }

        private static Dictionary<FuzzyTerm, double> getMembershipValuesThatAreGreaterZero(FuzzyVariable fuzzyVariable, NumericValue value)
        {
            var values = new Dictionary<FuzzyTerm, double>();
            foreach (var term in fuzzyVariable.FuzzyTerms)
            {
                var membershipValue = term.MembershipFunction.Apply(value.Value);
                if (membershipValue > 0)
                {
                    values.Add(term, membershipValue);
                }
            }
            return values;
        }

        private readonly IList<FuzzyVariable> _fuzzyVariables;
    }
}
