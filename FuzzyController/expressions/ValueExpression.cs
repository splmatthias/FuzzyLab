using System;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;

namespace fuzzyController.expressions
{
    public class ValueExpression : IFuzzyExpression
    {
        public FuzzyVariable Variable { get; private set; }

        public FuzzyTerm Value { get; private set; }

        public ValueExpression(FuzzyVariable variable, FuzzyTerm value)
        {
            if (variable == null)
                throw new ArgumentNullException("variable");
            if (value == null)
                throw new ArgumentNullException("value");

            Variable = variable;
            Value = value;
        }

        public T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
