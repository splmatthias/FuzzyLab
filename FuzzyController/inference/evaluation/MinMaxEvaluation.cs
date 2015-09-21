using System;

namespace fuzzyController.inference.evaluation
{
    [Default]
    public class MinMaxEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            return Math.Min(leftValue, rightValue);
        }

        public double Or(double leftValue, double rightValue)
        {
            return Math.Max(leftValue, rightValue);
        }

        public override string ToString()
        {
            return "MinMax Evaluation";
        }
    }
}
