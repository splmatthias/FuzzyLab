using System;
using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using NUnit.Framework;
using Rhino.Mocks;

namespace fuzzyController.test.expressions
{
    [TestFixture]
    public class FuzzyImplicationTest
    {
        [Test]
        public void Constructor()
        {
            var termA = new FuzzyTerm("TermA", new MembershipFunction());
            var termB = new FuzzyTerm("TermB", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA);
            var varB = new FuzzyVariable("Variable B", null, termB);

            var exprA = new ValueExpression(varA, termA);
            var exprB = new ValueExpression(varB, termB);

            var sut = new FuzzyImplication(exprA, exprB);

            Assert.AreEqual(exprA, sut.Premise);
            Assert.AreEqual(exprB, sut.Conclusion);
        }

        [Test]
        public void Constructor_Fails()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var expr = new ValueExpression(var, term);

            Assert.AreEqual("premise", Assert.Throws<ArgumentNullException>(() => new FuzzyImplication(null, expr)).ParamName);
            Assert.AreEqual("conclusion", Assert.Throws<ArgumentNullException>(() => new FuzzyImplication(expr, null)).ParamName);
        }

        [Test]
        public void Accept()
        {
            var termA = new FuzzyTerm("TermA", new MembershipFunction());
            var termB = new FuzzyTerm("TermB", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA);
            var varB = new FuzzyVariable("Variable B", null, termB);

            var mocks = new MockRepository();

            var visitor = mocks.StrictMock<IExpressionVisitor<int>>();

            var sut = new FuzzyImplication(new ValueExpression(varA, termA), new ValueExpression(varB, termB));

            Expect.Call(visitor.Visit(sut)).Return(42);

            mocks.ReplayAll();

            var result = sut.Accept(visitor);

            Assert.AreEqual(42, result);

            mocks.VerifyAll();
        }
    }
}
