using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.variables;
using System.Linq;

namespace fuzzyController.defuzzifier
{
    public class Defuzzifier: IDefuzzifier
    {
        public Defuzzifier(IMsfScalingStrategy msfScalingStrategy, IMsfMergingStrategy msfMergingStrategy, IDefuzzifyStrategy defuzzifyStrategy)
        {
            _msfScalingStrategy = msfScalingStrategy;
            _msfMergingStrategy = msfMergingStrategy;
            _defuzzifyStrategy = defuzzifyStrategy;
        }

        public DefuzzifiedValue Apply(FuzzyValue fuzzyValue)
        {
            var scaledMembershipFunctions = fuzzyValue.Values.Select(kvp => _msfScalingStrategy.Apply(kvp.Key.MembershipFunction, kvp.Value)).ToList();
            
            var mergeMembershipFunction = _msfMergingStrategy.Apply(scaledMembershipFunctions);

            var value = _defuzzifyStrategy.Apply(fuzzyValue.AssociatedVariable.NumericVariable, mergeMembershipFunction);

            return new DefuzzifiedValue(fuzzyValue.AssociatedVariable, mergeMembershipFunction, value);
        }

        private readonly IMsfScalingStrategy _msfScalingStrategy;
        private readonly IMsfMergingStrategy _msfMergingStrategy;
        private readonly IDefuzzifyStrategy _defuzzifyStrategy;
    }
}
