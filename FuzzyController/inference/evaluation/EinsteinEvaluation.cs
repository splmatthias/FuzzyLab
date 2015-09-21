namespace fuzzyController.inference.evaluation
{
    public class EinsteinEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            return (leftValue*rightValue)/(2 - (leftValue + rightValue - leftValue*rightValue));
        }

        public double Or(double leftValue, double rightValue)
        {
            return (leftValue + rightValue)/(1 + leftValue*rightValue);
        }

        public override string ToString()
        {
            return "Einstein Evaluation";
        }
    }
}
