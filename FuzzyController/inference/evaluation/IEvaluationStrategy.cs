namespace fuzzyController.inference.evaluation
{
    public interface IEvaluationStrategy
    {
        double And(double leftValue, double rightValue);
        double Or(double leftValue, double rightValue);
    }
}
