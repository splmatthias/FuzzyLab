using System;
using fuzzyController.expressions.visitors;

namespace fuzzyController.expressions
{
    public class FuzzyImplication : IFuzzyExpression
    {
        public IFuzzyExpression Premise { get; private set; }

        public ValueExpression Conclusion { get; private set; }

        public FuzzyImplication(IFuzzyExpression premise, ValueExpression conclusion)
        {
            if(premise == null)
                throw new ArgumentNullException("premise");
            if(conclusion == null)
                throw new ArgumentNullException("conclusion");

            Premise = premise;
            Conclusion = conclusion;
        }

        public T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
