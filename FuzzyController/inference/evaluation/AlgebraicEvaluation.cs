namespace fuzzyController.inference.evaluation
{
    public class AlgebraicEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            return leftValue*rightValue;
        }

        public double Or(double leftValue, double rightValue)
        {
            return leftValue + rightValue - leftValue*rightValue;
        }

        public override string ToString()
        {
            return "Algebraic Evaluation";
        }
    }
}
