namespace fuzzyController.inference.evaluation
{
    public class HamacherEvaluation : IEvaluationStrategy
    {
        public double And(double leftValue, double rightValue)
        {
            return (leftValue*rightValue)/(leftValue + rightValue - leftValue*rightValue);
        }

        public double Or(double leftValue, double rightValue)
        {
            return (leftValue + rightValue - 2*leftValue*rightValue)/(1 - leftValue*rightValue);
        }

        public override string ToString()
        {
            return "Hamacher Evaluation";
        }
    }
}
