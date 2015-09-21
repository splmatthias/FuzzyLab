using fuzzyController.math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.variables
{
    public class MembershipFunction : SortedList<double, double>
    {
        public Point this[int index]
        {
            get { return new Point(Keys[index], Values[index]); }
        }

        public double Apply(double value)
        {
            double memberShip;
            if (TryGetValue(value, out memberShip))
                return memberShip;

            KeyValuePair<double, double>? leftInterval = getLeftFromValue(value);
            KeyValuePair<double, double>? rightInterval = getRightFromValue(value);
            
            return getInterpolatedValue(value, rightInterval, leftInterval);
        }

        public MembershipFunction ClearUp()
        {
            var result = new MembershipFunction();

            if (Count == 1)
            {
                return this;
            }
            if (Count == 2)
            {
                if (Math.Abs(Values[0] - Values[1]) < 0.00000000001)
                    result.Add(Keys[0], Values[0]);
                else
                    return this;
            }
            else
            {
                result.Add(double.NegativeInfinity, Values[0]);
                foreach (var point in this)
                    result.Add(point.Key, point.Value);
                result.Add(double.PositiveInfinity, Values[Count-1]);

                for (var i = 1; i < result.Count - 1; i++)
                {
                    var previous = i-1;
                    var current = i;
                    var next = i+1;

                    if (Math.Abs(result.Values[previous] - result.Values[current]) < 0.00000000001
                        && Math.Abs(result.Values[current] - result.Values[next]) < 0.00000000001)
                    {
                        result.RemoveAt(current);
                        i--;
                    }
                    else
                    {
                        var a1 = result.Keys[next] - result.Keys[previous];
                        var a2 = result.Values[next] - result.Values[previous];
                        var b1 = result.Keys[current] - result.Keys[previous];
                        var b2 = result.Values[current] - result.Values[previous];
                        if (Math.Abs(a1/a2 - b1/b2) < 0.00000000001)
                        {
                            result.RemoveAt(current);
                            i--;
                        }
                    }
                }
                result.RemoveAt(0);
                result.RemoveAt(result.Count - 1);
            }

            return result;
        }

        public bool Equals(MembershipFunction other)
        {
            return Count == other.Count 
                && Keys.All( key => 
                        other.ContainsKey(key) 
                    && !(Math.Abs(this[key] - other[key]) > 0.00000000001));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MembershipFunction)obj);
        }

        public override string ToString()
        {
            return "{ " + string.Join(" | ", this.Select(kvp => "(" + kvp.Key + ";" + kvp.Value + ")")) + " }";
        }

        private KeyValuePair<double, double>? getLeftFromValue(double value)
        {
            foreach (var point in this.Reverse())
            {
                if (point.Key < value)
                {
                    return point;
                }
            }
            return null;
        }

        private KeyValuePair<double, double>? getRightFromValue(double value)
        {
            foreach (var point in this)
            {
                if (point.Key > value)
                {
                    return point;
                }
            }
            return null;
        }

        private static double getInterpolatedValue(double value, KeyValuePair<double, double>? rightInterval, KeyValuePair<double, double>? leftInterval)
        {
            if (leftInterval == null && rightInterval == null)
                return 0;
            if (leftInterval == null)
                return rightInterval.Value.Value;
            if (rightInterval == null)
                return leftInterval.Value.Value;
            
            double intervalWidth = rightInterval.Value.Key - leftInterval.Value.Key;
            double intervalHeight = rightInterval.Value.Value - leftInterval.Value.Value;
            double m = intervalHeight / intervalWidth;

            double relX = value - leftInterval.Value.Key;

            return leftInterval.Value.Value + relX * m;
        }
    }
}
