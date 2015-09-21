using System;
using fuzzyController.math;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    // ReSharper disable PossibleInvalidOperationException
    [Default]
    public class CoGDefuzzifyStrategy : IDefuzzifyStrategy
    {
        public double Apply(NumericVariable numericVariable, MembershipFunction msf)
        {
            if (msf.Count == 0)
                return (numericVariable.MaxValue - numericVariable.MinValue) / 2.0;
            if (msf[0].Y > 0 && msf[0].X > numericVariable.MinValue)
            {
                msf.Add(numericVariable.MinValue, msf[0].Y);
            }
            if (msf[msf.Count - 1].Y > 0 && msf[msf.Count - 1].X < numericVariable.MaxValue)
            {
                msf.Add(numericVariable.MaxValue, msf[msf.Count - 1].Y);
            }

            var numerator = 0d;
            var denominator = 0d;
            for (var i = 0; i < msf.Count - 1; i++)
            {
                var value1 = msf[i];
                var value2 = msf[i + 1];

                var min = Math.Min(value1.Y, value2.Y);
                var max = Math.Max(value1.Y, value2.Y);
                
                var line = new LineSegment(value1, value2);
                var m = line.Gradient.Value;
                var b = line.B.Value;
                numerator += ((m / 3.0) * (Math.Pow(value2.X, 3) - Math.Pow(value1.X, 3))) + ((b / 2.0) * (Math.Pow(value2.X, 2) - Math.Pow(value1.X, 2)));
                denominator += ((max + min) / 2.0) * (value2.X - value1.X);

            }
            return numerator / denominator;
        }
    }
}
