using System.Collections.Generic;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfMergingStrategy
{
    public interface IMsfMergingStrategy
    {
        MembershipFunction Apply(IList<MembershipFunction> msfs);
    }
}