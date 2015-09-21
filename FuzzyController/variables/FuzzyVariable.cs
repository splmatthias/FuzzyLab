using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzzyController.variables
{
    public class FuzzyVariable
    {
        public FuzzyVariable(string identifier, NumericVariable numericVariable, params FuzzyTerm[] fuzzyTerms)
        {
            if (string.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier");

            Identifier = identifier;
            Defuzzify = false;
            NumericVariable = numericVariable;
            FuzzyTerms = fuzzyTerms;
        }

        public readonly string Identifier;

        public readonly NumericVariable NumericVariable;

        public readonly IEnumerable<FuzzyTerm> FuzzyTerms;

        public readonly bool Defuzzify;

        public override string ToString()
        {
            var str = new StringBuilder(Identifier + " = { ");
            str.Append(string.Join(", ", FuzzyTerms.Select(t => t.ToString())));
            str.Append(" }");

            return str.ToString();
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as FuzzyVariable);
        }

        public bool Equals(FuzzyVariable obj)
        {
            return obj != null && obj.Identifier == Identifier;
        }
    }
}
