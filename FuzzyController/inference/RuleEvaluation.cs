using fuzzyController.expections;
using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger;
using fuzzyController.variables;
using System.Collections.Generic;
using System.Linq;

namespace fuzzyController.inference
{
    public class RuleEvaluation : IExpressionVisitor<double>, IRuleEvaluation
    {
        public RuleEvaluation(IEvaluationStrategy evaluationStrategy, IFuzzyValueMerger valueMerger)
        {
            _evaluationStrategy = evaluationStrategy;
            _valueMerger = valueMerger;
        }

        public Scope InputScope { get; set; }

        public Scope Apply(Scope scope, IEnumerable<FuzzyImplication> implications)
        {
            InputScope = scope;
            
            var fuzzyValues = implications.Select(apply).ToList();

            foreach (var value in scope)
            {
                var newValue = fuzzyValues.Find(val => val.AssociatedVariable.Equals(value.AssociatedVariable));
                if(newValue != null)
                {
                    foreach (var val in value.Values.Keys.Where(term => !newValue.Values.ContainsKey(term)))
                        newValue.Values.Add(val, value.Values[val]);
                }
                else
                {
                    fuzzyValues.Insert(0, value);
                }
            }

            return new Scope(_valueMerger.Apply(fuzzyValues));
        }

        public double Visit(ValueExpression expr)
        {
            FuzzyValue value = getValueForVariable(expr.Variable);

            if (value == null)
                throw new UnresolvedVariableException(expr.Variable);

            double result;
            if (value.Values.TryGetValue(expr.Value, out result))
                return result;
            return 0;
        }
        
        public double Visit(NotExpression expr)
        {
            return 1 - expr.Expression.Accept(this);
        }

        public double Visit(AndExpression expr)
        {
            return _evaluationStrategy.And(expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this));
        }

        public double Visit(OrExpression expr)
        {
            return _evaluationStrategy.Or(expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this));
        }

        public double Visit(FuzzyImplication implication)
        {
            return implication.Premise.Accept(this);
        }

        private FuzzyValue apply(FuzzyImplication implication)
        {
            double value = implication.Accept(this);

            return createFuzzyValue(implication.Conclusion.Variable, implication.Conclusion.Value, value);
        }

        private static FuzzyValue createFuzzyValue(FuzzyVariable variable, FuzzyTerm term, double value)
        {
            return new FuzzyValue(variable, new Dictionary<FuzzyTerm, double> {{term, value}});
        }

        private FuzzyValue getValueForVariable(FuzzyVariable variable)
        {
            return InputScope.FirstOrDefault(value => value.AssociatedVariable.Equals(variable));
        }

        private readonly IEvaluationStrategy _evaluationStrategy;
        private readonly IFuzzyValueMerger _valueMerger;
    }
}
