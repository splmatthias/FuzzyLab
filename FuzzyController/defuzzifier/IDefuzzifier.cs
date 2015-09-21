using fuzzyController.variables;

namespace fuzzyController.defuzzifier
{
    public interface IDefuzzifier
    {
        DefuzzifiedValue Apply(FuzzyValue fuzzyValue);
    }
}
    