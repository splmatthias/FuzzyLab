using System;

namespace fuzzyController.inference.evaluation
{
    public class DrasticEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            if (Math.Abs(Math.Max(leftValue, rightValue) - 1) < 0.0000000001)
                return Math.Min(leftValue, rightValue);
            return 0;
        }

        public double Or(double leftValue, double rightValue)
        {
            if (Math.Abs(Math.Min(leftValue, rightValue)) < 0.0000000001)
                return Math.Max(leftValue, rightValue);
            return 1;
        }

        public override string ToString()
        {
            return "Drastic Evaluation";
        }
    }
}
