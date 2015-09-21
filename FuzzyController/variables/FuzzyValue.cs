using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzzyController.variables
{
    public class FuzzyValue
    {
        public FuzzyValue(FuzzyVariable associatedVariable, IDictionary<FuzzyTerm, double> values)
        {
            if (associatedVariable == null)
                throw new ArgumentNullException("associatedVariable");

            AssociatedVariable = associatedVariable;
            Values = values;
        }

        public readonly FuzzyVariable AssociatedVariable;

        public readonly IDictionary<FuzzyTerm, double> Values;

        public override string ToString()
        {
            var str = new StringBuilder(AssociatedVariable.Identifier + " = ( ");
            str.Append(string.Join(", ", Values.Select(kv => kv.Key + "=" + kv.Value)));
            str.Append(" )");

            return str.ToString();
        }

        public override int GetHashCode()
        {
            return AssociatedVariable.GetHashCode();
        }
    }
}
