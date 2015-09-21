using fuzzyController.variables;

namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    public interface IDefuzzifyStrategy
    {
        double Apply(NumericVariable numericVariable, MembershipFunction msf);
    }
}