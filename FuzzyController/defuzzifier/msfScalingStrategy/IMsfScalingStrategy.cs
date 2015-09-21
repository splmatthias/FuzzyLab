using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfScalingStrategy
{
    public interface IMsfScalingStrategy
    {
        MembershipFunction Apply(MembershipFunction msf, double value);
    }
}