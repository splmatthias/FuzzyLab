namespace fuzzyController.expressions.visitors
{
    public interface IExpressionVisitor<out T>
    {
        T Visit(ValueExpression expr);
        T Visit(NotExpression expr);
        T Visit(AndExpression expr);
        T Visit(OrExpression expr);
        T Visit(FuzzyImplication implication);
    }
}
