using System;

namespace fuzzyController.inference.evaluation
{
    public class LukasiewiczEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            return Math.Max(0, leftValue + rightValue - 1);
        }

        public double Or(double leftValue, double rightValue)
        {
            return Math.Min(1, leftValue + rightValue);
        }

        public override string ToString()
        {
            return "Lukasiewicz Evaluation";
        }
    }
}
