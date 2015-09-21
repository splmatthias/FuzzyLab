using System;
using fuzzyController.expressions.visitors;

namespace fuzzyController.expressions
{
    public class NotExpression : IFuzzyExpression
    {
        public IFuzzyExpression Expression { get; private set; }

        public NotExpression(IFuzzyExpression expression)
        {
            if(expression == null)
                throw new ArgumentNullException("expression");

            Expression = expression;
        }

        public T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
