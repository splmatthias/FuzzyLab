using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.expressions.visitors
{
    [TestFixture]
    public class ToStringVisitorTest
    {
        [Test]
        public void VisitValue()
        {
            var valueExpr =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable", new NumericVariable("MyNumVariable")),
                    new FuzzyTerm("MyTerm", new MembershipFunction()));

            var sut = new ToStringVisitor();

            var result = sut.Visit(valueExpr);
            Assert.AreEqual("MyFuzzyVariable=MyTerm", result);

            result = valueExpr.Accept(sut);
            Assert.AreEqual("MyFuzzyVariable=MyTerm", result);
        }

        [Test]
        public void VisitNotValue()
        {
            var valueExpr =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable", new NumericVariable("MyNumVariable")),
                    new FuzzyTerm("MyTerm", new MembershipFunction()));

            var notExpr = new NotExpression(valueExpr);

            var sut = new ToStringVisitor();

            var result = sut.Visit(notExpr);
            Assert.AreEqual("!( MyFuzzyVariable=MyTerm )", result);

            result = notExpr.Accept(sut);
            Assert.AreEqual("!( MyFuzzyVariable=MyTerm )", result);
        }

        [Test]
        public void VisitAnd()
        {
            var valueExpr1 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1")),
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2")),
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var andExpr = new AndExpression(valueExpr1, valueExpr2);

            var sut = new ToStringVisitor();

            var result = sut.Visit(andExpr);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 && MyFuzzyVariable2=MyTerm2", result);

            result = andExpr.Accept(sut);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 && MyFuzzyVariable2=MyTerm2", result);
        }

        [Test]
        public void VisitOr()
        {
            var valueExpr1 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1")),
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2")),
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var orExpr = new OrExpression(valueExpr1, valueExpr2);

            var sut = new ToStringVisitor();

            var result = sut.Visit(orExpr);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 || MyFuzzyVariable2=MyTerm2", result);

            result = orExpr.Accept(sut);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 || MyFuzzyVariable2=MyTerm2", result);
        }

        [Test]
        public void VisitRule()
        {
            var valueExpr1 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable1", new NumericVariable("MyNumVariable1")),
                    new FuzzyTerm("MyTerm1", new MembershipFunction()));
            var valueExpr2 =
                new ValueExpression(
                    new FuzzyVariable("MyFuzzyVariable2", new NumericVariable("MyNumVariable2")),
                    new FuzzyTerm("MyTerm2", new MembershipFunction()));

            var ruleExpr = new FuzzyImplication(valueExpr1, valueExpr2);

            var sut = new ToStringVisitor();

            var result = sut.Visit(ruleExpr);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 => MyFuzzyVariable2=MyTerm2", result);

            result = ruleExpr.Accept(sut);
            Assert.AreEqual("MyFuzzyVariable1=MyTerm1 => MyFuzzyVariable2=MyTerm2", result);
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

            var sut = new ToStringVisitor();

            var result = sut.Visit(ruleExpr);
            Assert.AreEqual("!( A=x && B=y ) || ( A=y && B=x ) => C=z", result);

            result = ruleExpr.Accept(sut);
            Assert.AreEqual("!( A=x && B=y ) || ( A=y && B=x ) => C=z", result);
        }
    }
}
