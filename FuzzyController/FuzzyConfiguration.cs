using fuzzyController.variables;
using System.Collections.Generic;

namespace fuzzyController
{
    public class FuzzyConfiguration
    {
        public FuzzyConfiguration(IList<NumericVariable> numericVariables,
                                  IList<FuzzyVariable> fuzzyVariables,
                                  IList<FuzzyVariable> fuzzification, 
                                  IList<Iteration> iterations,
                                  IList<FuzzyVariable> defuzzification)
        {
            NumericVariables = numericVariables;
            FuzzyVariables = fuzzyVariables;
            Fuzzification = fuzzification;
            Iterations = iterations;
            Defuzzification = defuzzification;
        }

        public readonly IList<NumericVariable> NumericVariables;

        public readonly IList<FuzzyVariable> FuzzyVariables;

        public readonly IList<FuzzyVariable> Fuzzification; 

        public readonly IList<Iteration> Iterations;

        public readonly IList<FuzzyVariable> Defuzzification;
    }
}
