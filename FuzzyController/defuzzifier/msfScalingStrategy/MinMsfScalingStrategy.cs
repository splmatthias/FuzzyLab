using System;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfScalingStrategy
{
    [Default]
    public class MinMsfScalingStrategy : IMsfScalingStrategy
    {
        /// <summary>
        /// Mamdani-implication
        /// </summary>
        /// <param name="msf"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public MembershipFunction Apply(MembershipFunction msf, double minValue)
        {
            var result = new MembershipFunction();

            var count = msf.Count;

            if(count == 0)
                return msf;

            if (count == 1)
            {
                foreach (var point in msf)
                    result.Add(point.Key, Math.Min(point.Value, minValue));
            }
            else
            {
                for (var i = 0; i < count - 1; i++)
                {
                    var key = msf.Keys[i];
                    var nextKey = msf.Keys[i + 1];
                    var value = msf.Values[i];
                    var nextValue = msf.Values[i + 1];

                    if (value <= minValue)
                    {
                        result.Add(key, value);
                        if (nextValue > minValue)
                        {
                            var m = (nextValue - value)/(nextKey - key);

                            var newKey = key + minValue/m;
                            if(!result.Keys.Contains(newKey))
                                result.Add(newKey, minValue);
                        }
                    }
                    else if (nextValue < minValue)
                    {
                        var m = (nextValue - value)/(nextKey - key);

                        var newKey = key + (minValue - value)/m;

                        result.Add(newKey, minValue);
                    }
                }
                if (msf.Values[count - 1] <= minValue)
                {
                    result.Add(msf.Keys[count - 1], msf.Values[count - 1]);
                }
            }

            return result.ClearUp();
        }

        public override string ToString()
        {
            return "Minumum Msf Scaling";
        }
    }
}
