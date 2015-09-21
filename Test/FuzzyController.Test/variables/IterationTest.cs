using fuzzyController.expressions;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class IterationTest
    {
        [Test]
        public void Constructor()
        {
            var num1 = new NumericVariable("Variable1");
            var num2 = new NumericVariable("Variable2");
            var term1 = new FuzzyTerm("Var1_Term1", new MembershipFunction());
            var term2 = new FuzzyTerm("Var2_Term1", new MembershipFunction());
            var var1 = new FuzzyVariable("Variable1", num1, term1);
            var var2 = new FuzzyVariable("Variable2", num2, term2);
            var rule1 = new FuzzyImplication(new ValueExpression(var1, term1), new ValueExpression(var1, term1));
            var rule2 = new FuzzyImplication(new ValueExpression(var2, term2), new ValueExpression(var2, term2));

            var sut = new Iteration(new[] { rule1, rule2 });
            Assert.AreEqual(rule1, sut.Implications[0]);
            Assert.AreEqual(rule2, sut.Implications[1]);
        }
    }
}
