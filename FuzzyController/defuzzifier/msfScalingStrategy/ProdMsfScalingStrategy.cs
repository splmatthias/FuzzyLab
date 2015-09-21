using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfScalingStrategy
{
    public class ProdMsfScalingStrategy : IMsfScalingStrategy
    {
        public MembershipFunction Apply(MembershipFunction msf, double value)
        {
            var result = new MembershipFunction();

            foreach (var point in msf)
            {
                result.Add(point.Key, value*point.Value);
            }

            return result;
        }

        public override string ToString()
        {
            return "Product Msf Scaling";
        }
    }
}
