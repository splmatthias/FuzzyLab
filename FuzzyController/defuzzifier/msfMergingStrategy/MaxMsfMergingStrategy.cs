using System;
using System.Collections.Generic;
using System.Linq;
using fuzzyController.math;
using fuzzyController.variables;

namespace fuzzyController.defuzzifier.msfMergingStrategy
{
    [Default]
    public class MaxMsfMergingStrategy : IMsfMergingStrategy
    {
        public MembershipFunction Apply(IList<MembershipFunction> membershipFunctions)
        {
            var combinedFunctions = membershipFunctions;

            while (combinedFunctions.Count >= 2)
            {
                combinedFunctions = mergeFirstTwoFunctions(combinedFunctions);
            }
            return firstOrEmptyFunction(combinedFunctions);
        }

        private static IList<MembershipFunction> mergeFirstTwoFunctions(IList<MembershipFunction> msfs)
        {
            var combined = combine(msfs[0], msfs[1]);
            var newMsfs = new List<MembershipFunction> {combined};
            newMsfs.AddRange(msfs.Skip(2));
            return newMsfs;
        }

        private static MembershipFunction combine(MembershipFunction first, MembershipFunction second)
        {
            if (!first.Any())
                return second;
            if (!second.Any())
                return first;
            
            var scanPoints = getScanPoints(first, second);
            
            var result = new MembershipFunction();
            for (var i = 0; i < scanPoints.Count; i++)
            {
                var scanPoint = scanPoints[i];

                var valueFirst = first.Apply(scanPoint);
                var valueSecond = second.Apply(scanPoint);

                result.Add(scanPoint, Math.Max(valueFirst, valueSecond));
                
                // Check if there is an intersection between this scan point and the next
                // and add it to the list of scan points:
                var nextKeyFirst = getNextKey(first, scanPoint);
                var nextKeySecond = getNextKey(second, scanPoint);
                if (nextKeyFirst.HasValue && nextKeySecond.HasValue)
                {
                    var nextValueFirst = first[nextKeyFirst.Value];
                    var nextValueSecond = second[nextKeySecond.Value];

                    if (valueFirst >= valueSecond && nextValueFirst < nextValueSecond ||
                        valueFirst < valueSecond && nextValueFirst >= nextValueSecond)
                    {
                        var lineFirst = new LineSegment(new Point(scanPoint, valueFirst),
                            new Point(nextKeyFirst.Value, nextValueFirst));
                        var lineSecond = new LineSegment(new Point(scanPoint, valueSecond),
                            new Point(nextKeySecond.Value, nextValueSecond));

                        var intersection = lineFirst.Intersect(lineSecond);
                        if (intersection != null && !scanPoints.Contains(intersection.X) && intersection.X > scanPoint)
                            scanPoints.Insert(i + 1, intersection.X);
                    }
                }
            }

            return result.ClearUp();
        }

        private static List<double> getScanPoints(MembershipFunction first, MembershipFunction second)
        {
            var scanPoints = new List<double>();
            scanPoints.AddRange(first.Keys);
            foreach (var key in second.Keys.Where(key => !scanPoints.Contains(key)))
                scanPoints.Add(key);
            scanPoints.Sort();
            return scanPoints;
        }

        private static double? getNextKey(MembershipFunction membershipFunction, double start)
        {
            foreach (var key in membershipFunction.Keys.Where(key => start < key))
            {
                return key;
            }
            return null;
        }

        private static MembershipFunction firstOrEmptyFunction(IList<MembershipFunction> combinedFunctions)
        {
            return combinedFunctions.Any() ? combinedFunctions[0] : new MembershipFunction();
        }
        
        public override string ToString()
        {
            return "Maximum Msf Merge";
        }
    }
}
