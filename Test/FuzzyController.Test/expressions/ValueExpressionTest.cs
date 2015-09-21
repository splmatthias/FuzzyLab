using System;
using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using NUnit.Framework;
using Rhino.Mocks;

namespace fuzzyController.test.expressions
{
    [TestFixture]
    public class ValueExpressionTest
    {
        [Test]
        public void Constructor()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            var sut = new ValueExpression(var, term);

            Assert.AreEqual(var, sut.Variable);
            Assert.AreEqual(term, sut.Value);
        }

        [Test]
        public void Constructor_Fails()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);

            Assert.AreEqual("variable", Assert.Throws<ArgumentNullException>(() => new ValueExpression(null, term)).ParamName);
            Assert.AreEqual("value", Assert.Throws<ArgumentNullException>(() => new ValueExpression(var, null)).ParamName);
        }

        [Test]
        public void Accept()
        {
            var term = new FuzzyTerm("Term", new MembershipFunction());
            var var = new FuzzyVariable("Variable", null, term);
            
            var mocks = new MockRepository();

            var visitor = mocks.StrictMock<IExpressionVisitor<int>>();

            var sut = new ValueExpression(var, term);

            Expect.Call(visitor.Visit(sut)).Return(42);

            mocks.ReplayAll();

            var result = sut.Accept(visitor);

            Assert.AreEqual(42, result);

            mocks.VerifyAll();
        }
    }
}
