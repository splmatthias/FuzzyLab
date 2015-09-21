namespace fuzzyController.expressions.visitors
{
    public class ToStringVisitor : IExpressionVisitor<string>
    {
        public string Visit(ValueExpression expr)
        {
            return expr.Variable.Identifier + "=" + expr.Value.Term;
        }
        
        private string visit(IFuzzyExpression expr)
        {
            if (expr is BinaryExpression)
                return "( " + expr.Accept(this) + " )";

            return expr.Accept(this);
        }

        public string Visit(NotExpression expr)
        {
            return "!( " + expr.Expression.Accept(this) + " )";
        }
        
        public string Visit(AndExpression expr)
        {
            return visit(expr.LeftExpression) + " && " + visit(expr.RightExpression);
        }

        public string Visit(OrExpression expr)
        {
            return visit(expr.LeftExpression) + " || " + visit(expr.RightExpression);
        }

        public string Visit(FuzzyImplication implication)
        {
            return implication.Premise.Accept(this)
                   + " => "
                   + implication.Conclusion.Variable.Identifier
                   + "=" + implication.Conclusion.Value.Term;
        }
    }
}
