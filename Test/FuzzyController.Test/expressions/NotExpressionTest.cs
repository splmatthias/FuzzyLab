using System;
using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using NUnit.Framework;
using Rhino.Mocks;

namespace fuzzyController.test.expressions
{
    [TestFixture]
    public class NotExpressionTest
    {
        [Test]
        public void Constructor()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var expr = new ValueExpression(var, term);
            var sut = new NotExpression(expr);

            Assert.AreEqual(expr, sut.Expression);
        }

        [Test]
        public void Constructor_Fails()
        {
            Assert.AreEqual("expression", Assert.Throws<ArgumentNullException>(() => new NotExpression(null)).ParamName);
        }

        [Test]
        public void Accept()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var expr = new ValueExpression(var, term);

            var mocks = new MockRepository();

            var visitor = mocks.StrictMock<IExpressionVisitor<int>>();

            var sut = new NotExpression(expr);

            Expect.Call(visitor.Visit(sut)).Return(42);

            mocks.ReplayAll();

            var result = sut.Accept(visitor);

            Assert.AreEqual(42, result);

            mocks.VerifyAll();
        }
    }
}
