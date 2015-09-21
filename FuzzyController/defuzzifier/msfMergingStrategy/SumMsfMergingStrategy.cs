using System.Collections.Generic;
using System.Linq;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfMergingStrategy
{
    public class SumMsfMergingStrategy: IMsfMergingStrategy
    {
        public MembershipFunction Apply(IList<MembershipFunction> membershipFunctions)
        {
            var result = new MembershipFunction();

            var msfs = membershipFunctions;

            while (msfs.Count > 1)
            {
                var combined = combine(msfs[0], msfs[1]);
                var newMsfs = new List<MembershipFunction> { combined };
                newMsfs.AddRange(msfs.Skip(2));
                msfs = newMsfs;
            }
            if (msfs.Any())
                return msfs[0];

            return result;
        }
        
        private MembershipFunction combine(MembershipFunction first, MembershipFunction second)
        {
            if (!first.Any())
                return second;
            if (!second.Any())
                return first;


            var scanPoints = new List<double>();
            scanPoints.AddRange(first.Keys);
            foreach (var key in second.Keys.Where(key => !scanPoints.Contains(key)))
                scanPoints.Add(key);
            scanPoints.Sort();



            var result = new MembershipFunction();
            foreach (var scanPoint in scanPoints)
            {
                var valueFirst = first.Apply(scanPoint);
                var valueSecond = second.Apply(scanPoint);

                result.Add(scanPoint, valueFirst + valueSecond);
            }

            return result.ClearUp();
        }

        public override string ToString()
        {
            return "Sum Msf Merge";
        }
    }
}
