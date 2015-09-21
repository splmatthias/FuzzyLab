using System;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    public abstract class MaximumDefuzzifyStrategy : IDefuzzifyStrategy
    {
        public double Apply(NumericVariable numericVariable, MembershipFunction msf)
        {
            var maximum = double.NegativeInfinity;
            var left = 0d;
            var right = 0d;
            foreach (var item in msf)
            {
                if (item.Value > maximum)
                {
                    maximum = item.Value;
                    left = item.Key;
                    right = item.Key;
                }
                else if(Math.Abs(item.Value - maximum) < 0.00000000001)
                {
                    right = item.Key;
                }
            }
            var first = msf[0];
            if (Math.Abs(first.Y - maximum) < 0.00000000001)
            {
                left = numericVariable.MinValue;
            }
            var last = msf[msf.Count -1];
            if (Math.Abs(last.Y - maximum) < 0.00000000001)
            {
                right = numericVariable.MaxValue;
            }

            return getValue(left, right);
        }

        protected abstract double getValue(double left, double right);
    }
}
