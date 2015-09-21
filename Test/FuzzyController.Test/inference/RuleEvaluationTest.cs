using System;
using System.Collections.Generic;
using System.Linq;
using fuzzyController.expections;
using fuzzyController.expressions;
using fuzzyController.inference;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger;
using fuzzyController.inference.valueMerger.strategies;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.inference
{
    [TestFixture]
    public class RuleEvaluationTest
    {
        private static IEvaluationStrategy createEvaluationStrategy(string strategyName)
        {
            switch (strategyName)
            {
                case "Algebraic":
                    return new AlgebraicEvaluation();
                case "Drastic":
                    return new DrasticEvaluation();
                case "Einstein":
                    return new EinsteinEvaluation();
                case "Hamacher":
                    return new HamacherEvaluation();
                case "Lukasiewicz":
                    return new LukasiewiczEvaluation();
                case "MinMax":
                    return new MinMaxEvaluation();
            }
            throw new ArgumentException("Unknown strategy name");
        }

        private static IFuzzyValueMerger createFuzzyValueMerger(string strategyName)
        {
            switch (strategyName)
            {
                case "Average":
                    return new FuzzyValueMerger(new AverageValueStrategy());
                case "Min":
                    return new FuzzyValueMerger(new MinValueStrategy());
                case "Max":
                    return new FuzzyValueMerger(new MaxValueStrategy());
                case "Sum":
                    return new FuzzyValueMerger(new SumValueStrategy());
            }
            throw new ArgumentException("Unknown strategy name");
        }

        [Test]
        public void SetCurrentScope(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var value = new FuzzyValue(var, new Dictionary<FuzzyTerm, double> { { term, 0.2 } });
            var scope = new Scope(value);
            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = scope };

            Assert.AreEqual(scope, sut.InputScope);
        }

        [Test]
        public void Visit_Value_Expression(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var value = new FuzzyValue(var, new Dictionary<FuzzyTerm, double> { { term, 0.2 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var result = sut.Visit(new ValueExpression(var, term));

            Assert.AreEqual(0.2, result);
        }

        [Test]
        public void Visit_Value_Expression_Fails_Variable_Not_Found(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA = new FuzzyTerm("Term A", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA);
            var termB = new FuzzyTerm("Term B", new MembershipFunction());
            var varB = new FuzzyVariable("Variable B", null);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA, 0.2 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            Assert.Throws<UnresolvedVariableException>(() => sut.Visit(new ValueExpression(varB, termB)));
        }

        [Test]
        public void Visit_Not_Expression(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var value = new FuzzyValue(var, new Dictionary<FuzzyTerm, double> { { term, 0.2 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var result = sut.Visit(new NotExpression(new ValueExpression(var, term)));

            Assert.AreEqual(0.8, result);
        }

        [Test]
        public void Visit_And_Expression(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var term1 = new FuzzyTerm("Term1", new MembershipFunction());
            var term2 = new FuzzyTerm("Term2", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term1, term2);

            var value = new FuzzyValue(var, new Dictionary<FuzzyTerm, double> { { term1, 0.2 }, { term2, 0.8 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var result = sut.Visit(new AndExpression(new ValueExpression(var, term1), new ValueExpression(var, term2)));

            Assert.AreEqual(evaluationStrategy.And(value.Values[term1], value.Values[term2]), result);
        }

        [Test]
        public void Visit_Or_Expression(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var term1 = new FuzzyTerm("Term1", new MembershipFunction());
            var term2 = new FuzzyTerm("Term2", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term1, term2);

            var value = new FuzzyValue(var, new Dictionary<FuzzyTerm, double> { { term1, 0.2 }, { term2, 0.8 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var result = sut.Visit(new OrExpression(new ValueExpression(var, term1), new ValueExpression(var, term2)));

            Assert.AreEqual(evaluationStrategy.Or(value.Values[term1], value.Values[term2]), result);
        }

        [Test]
        public void Visit_Implication_Expression(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA = new FuzzyTerm("TermA", new MembershipFunction());
            var termB = new FuzzyTerm("TermB", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA);
            var varB = new FuzzyVariable("Variable B", null, termB);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA, 0.4 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var result = sut.Visit(new FuzzyImplication(new ValueExpression(varA, termA), new ValueExpression(varB, termB)));

            Assert.AreEqual(0.4, result);
        }

        [Test]
        public void Apply_One_Implication(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA = new FuzzyTerm("TermA", new MembershipFunction());
            var termB = new FuzzyTerm("TermB", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA);
            var varB = new FuzzyVariable("Variable B", null, termB);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA, 0.4 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger);

            var result = sut.Apply(new Scope(value), new[] {new FuzzyImplication(new ValueExpression(varA, termA), new ValueExpression(varB, termB))});

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(varA, result[0].AssociatedVariable);
            Assert.AreEqual(1, result[0].Values.Count);
            Assert.AreEqual(0.4, result[0].Values[termA]);
            Assert.AreEqual(varB, result[1].AssociatedVariable);
            Assert.AreEqual(1, result[1].Values.Count);
            Assert.AreEqual(0.4, result[1].Values[termB]);
        }

        [Test]
        public void Apply_Many_Implication(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA1 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termA2 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termB1 = new FuzzyTerm("TermB1", new MembershipFunction());
            var termB2 = new FuzzyTerm("TermB2", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA1, termA2);
            var varB = new FuzzyVariable("Variable B", null, termB1, termB2);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA1, 0.4 }, { termA2, 0.6 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger);

            var impl1 = new FuzzyImplication(new ValueExpression(varA, termA1), new ValueExpression(varB, termB1));
            var impl2 = new FuzzyImplication(new ValueExpression(varA, termA2), new ValueExpression(varB, termB2));

            var result = sut.Apply(new Scope(value), new[] {impl1, impl2}).ToList();

            Assert.AreEqual(2, result.Count);
            var valA = result.Find(val => val.AssociatedVariable.Equals(varA));
            Assert.IsNotNull(valA);
            Assert.AreEqual(2, valA.Values.Count);
            Assert.AreEqual(0.4, valA.Values[termA1]);
            Assert.AreEqual(0.6, valA.Values[termA2]);

            var valB = result.Find(val => val.AssociatedVariable.Equals(varB));
            Assert.IsNotNull(valB);
            Assert.AreEqual(2, valB.Values.Count);
            Assert.AreEqual(0.4, valB.Values[termB1]);
            Assert.AreEqual(0.6, valB.Values[termB2]);
        }

        [Test]
        public void Apply_Many_Implication_With_Value_Merging(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA1 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termA2 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termB1 = new FuzzyTerm("TermB1", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA1, termA2);
            var varB = new FuzzyVariable("Variable B", null, termB1);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA1, 0.5 }, { termA2, 0.6 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var impl1 = new FuzzyImplication(new ValueExpression(varA, termA1), new ValueExpression(varB, termB1));
            var impl2 = new FuzzyImplication(new ValueExpression(varA, termA2), new ValueExpression(varB, termB1));

            var result = sut.Apply(new Scope(value), new[] { impl1, impl2 });

            var mergedValue = merger.Apply(new FuzzyValue[]
            {
                new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB1, 0.5 } }),
                new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB1, 0.6 } })
            });

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(varA, result[0].AssociatedVariable);
            Assert.AreEqual(2, result[0].Values.Count);
            Assert.AreEqual(0.5, result[0].Values[termA1]);
            Assert.AreEqual(0.6, result[0].Values[termA2]);


            Assert.AreEqual(varB, result[1].AssociatedVariable);
            Assert.AreEqual(1, result[1].Values.Count);
            Assert.AreEqual(mergedValue[0].Values[termB1], result[1].Values[termB1]);
        }

        [Test]
        public void Apply_Many_Implication_With_Value_Override(
            [Values("Algebraic", "Drastic", "Einstein", "Hamacher", "Lukasiewicz", "MinMax")] string evalStrategyName,
            [Values("Average", "Max", "Min", "Sum")] string mergeStrategy)
        {
            var evaluationStrategy = createEvaluationStrategy(evalStrategyName);
            var merger = createFuzzyValueMerger(mergeStrategy);

            var termA1 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termA2 = new FuzzyTerm("TermA1", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA1, termA2);

            var value = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA1, 0.2 }, { termA2, 0.6 } });

            var sut = new RuleEvaluation(evaluationStrategy, merger) { InputScope = new Scope(value) };

            var impl1 = new FuzzyImplication(new AndExpression(new ValueExpression(varA, termA1), new ValueExpression(varA, termA2)), new ValueExpression(varA, termA1));

            var result = sut.Apply(new Scope(value), new[] { impl1 });

            var val = evaluationStrategy.And(0.2, 0.6);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(varA, result[0].AssociatedVariable);
            Assert.AreEqual(2, result[0].Values.Count);
            // A = A1 got overridden:
            Assert.AreEqual(val, result[0].Values[termA1]);
            // A = A2 stays the same:
            Assert.AreEqual(0.6, result[0].Values[termA2]);
        }
    }
}
