using fuzzyController.expressions.visitors;

namespace fuzzyController.expressions
{
    public interface IFuzzyExpression
    {
        T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
