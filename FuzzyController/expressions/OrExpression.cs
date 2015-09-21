using fuzzyController.expressions.visitors;

namespace fuzzyController.expressions
{
    public class OrExpression : BinaryExpression
    {
        public OrExpression(IFuzzyExpression leftExpression, IFuzzyExpression rightExpression)
            : base(leftExpression, rightExpression)
        { }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
