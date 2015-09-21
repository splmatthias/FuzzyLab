using fuzzyController.expections;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.exceptions
{
    [TestFixture]
    public class UnresolvedVariableExceptionTest
    {
        [Test]
        public void Constructor()
        {
            var variable = new FuzzyVariable("Var", null);
            var sut = new UnresolvedVariableException(variable);
            Assert.AreEqual(variable, sut.Variable);
        }
    }
}
