using System;
using fuzzyController.expressions.visitors;

namespace fuzzyController.expressions
{
    public abstract class BinaryExpression : IFuzzyExpression
    {
        public IFuzzyExpression LeftExpression { get; private set; }
        public IFuzzyExpression RightExpression { get; private set; }

        protected BinaryExpression(IFuzzyExpression leftExpression, IFuzzyExpression rightExpression)
        {
            if (leftExpression == null)
                throw new ArgumentNullException("leftExpression");
            if (rightExpression == null)
                throw new ArgumentNullException("rightExpression");

            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
