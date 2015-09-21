using System.Linq;
using fuzzyController.variables;
using System.Collections.Generic;

namespace fuzzyController.expressions.visitors
{
    public class GetInvolvedVariables : IExpressionVisitor<List<FuzzyVariable>>
    {
        public List<FuzzyVariable> Visit(ValueExpression expr)
        {
            return new List<FuzzyVariable> {expr.Variable};
        }

        public List<FuzzyVariable> Visit(NotExpression expr)
        {
            return expr.Expression.Accept(this);
        }

        private List<FuzzyVariable> concat(List<FuzzyVariable> first, List<FuzzyVariable> second)
        {
            var result = new List<FuzzyVariable>(first);
            foreach (var item in second.Where(item => !result.Contains(item)))
                result.Add(item);
            return result;
        }

        public List<FuzzyVariable> Visit(AndExpression expr)
        {
            return concat(expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this));
        }

        public List<FuzzyVariable> Visit(OrExpression expr)
        {
            return concat(expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this));
        }

        public List<FuzzyVariable> Visit(FuzzyImplication implication)
        {
            return concat(implication.Premise.Accept(this), implication.Conclusion.Accept(this));
        }
    }
}
