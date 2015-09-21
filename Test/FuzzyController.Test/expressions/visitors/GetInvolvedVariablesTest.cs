using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.expressions.visitors
{
    [TestFixture]
    public class GetInvolvedVariablesTest
    {
        [Test]
        public void VisitValue()
        {
            var variable = new FuzzyVariable("MyFuzzyVariable", new NumericVariable("MyNumVariable"));
            var valueExpr =
                new ValueExpression(
                    variable,
                    new FuzzyTerm("MyTerm", new MembershipFunction()));

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(valueExpr);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(variable, result[0]);

            result = valueExpr.Accept(sut);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(variable, result[0]);
        }

        [Test]
        public void VisitNotValue()
        {
            var variable = new FuzzyVariable("MyFuzzyVariable", new NumericVariable("MyNumVariable"));
            var valueExpr =
                new ValueExpression(
                    variable,
                    new FuzzyTerm("MyTerm", new MembershipFunction()));

            var notExpr = new NotExpression(valueExpr);

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(notExpr);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(variable, result[0]);

            result = notExpr.Accept(sut);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(variable, result[0]);
        }

        [Test]
        public void VisitAnd()
        {
            var variable1 = new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1"));
            var variable2 = new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2"));
            var valueExpr1 =
                new ValueExpression(
                    variable1,
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    variable2,
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var andExpr = new AndExpression(valueExpr1, valueExpr2);

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(andExpr);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));

            result = andExpr.Accept(sut);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));
        }

        [Test]
        public void VisitOr()
        {
            var variable1 = new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1"));
            var variable2 = new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2"));
            var valueExpr1 =
                new ValueExpression(
                    variable1,
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    variable2,
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var orExpr = new OrExpression(valueExpr1, valueExpr2);

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(orExpr);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));

            result = orExpr.Accept(sut);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));
        }

        [Test]
        public void VisitRule()
        {
            var variable1 = new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1"));
            var variable2 = new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2"));
            var valueExpr1 =
                new ValueExpression(
                    variable1,
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    variable2,
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var ruleExpr = new FuzzyImplication(valueExpr1, valueExpr2);

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(ruleExpr);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));

            result = ruleExpr.Accept(sut);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(variable1));
            Assert.IsTrue(result.Contains(variable2));
        }

        [Test]
        public void ComplexExpression()
        {
            var A = new FuzzyVariable("A", new NumericVariable("a"));
            var B = new FuzzyVariable("B", new NumericVariable("b"));
            var C = new FuzzyVariable("C", new NumericVariable("c"));

            var x = new FuzzyTerm("x", new MembershipFunction());
            var y = new FuzzyTerm("y", new MembershipFunction());
            var z = new FuzzyTerm("z", new MembershipFunction());


            var ruleExpr = new FuzzyImplication(
                new OrExpression(
                    new NotExpression(
                        new AndExpression(
                            new ValueExpression(A, x),
                            new ValueExpression(B, y)
                        )
                    ),
                    new AndExpression(
                        new ValueExpression(A, y),
                        new ValueExpression(B, x)
                    )
                ),
                new ValueExpression(C, z)
            );

            var sut = new GetInvolvedVariables();

            var result = sut.Visit(ruleExpr);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Contains(A));
            Assert.IsTrue(result.Contains(B));
            Assert.IsTrue(result.Contains(C));

            result = ruleExpr.Accept(sut);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Contains(A));
            Assert.IsTrue(result.Contains(B));
            Assert.IsTrue(result.Contains(C));
        }
    }
}
