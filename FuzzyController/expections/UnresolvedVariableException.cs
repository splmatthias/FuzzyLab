using fuzzyController.variables;
using System;

namespace fuzzyController.expections
{
    public class UnresolvedVariableException : Exception
    {
        public FuzzyVariable Variable { get; private set; }

        public UnresolvedVariableException(FuzzyVariable variable)
        {
            Variable = variable;
        }
    }
}
