namespace fuzzyController.variables
{
    public class DefuzzifiedValue
    {
        public DefuzzifiedValue(FuzzyVariable variable, MembershipFunction msf, double value)
        {
            Variable = variable;
            MembershipFunction = msf;
            Value = value;
        }

        public readonly FuzzyVariable Variable;

        public readonly MembershipFunction MembershipFunction;

        public readonly double Value;
    }
}
