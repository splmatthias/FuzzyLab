using System;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class NumericValueTest
    {
        [Test]
        public void Constructor()
        {
            var numVariable = new NumericVariable("MyVariable");

            var sut = new NumericValue(numVariable, 42);

            Assert.AreEqual(numVariable, sut.Variable);
            Assert.AreEqual(42, sut.Value);
        }

        [Test]
        public void ConstructorFails()
        {
            var numVariable = new NumericVariable("MyVariable", 42, 1701);
            
            Assert.AreEqual("value",
                Assert.Throws<ArgumentOutOfRangeException>(() => new NumericValue(numVariable, 13)).ParamName);
            
            Assert.AreEqual("variable", 
                Assert.Throws<ArgumentNullException>(() => new NumericValue(null, 42)).ParamName);
        }

        [Test]
        public void ToStringTest()
        {
            var numVariable = new NumericVariable("MyVariable");

            var sut = new NumericValue(numVariable, 42);

            Assert.AreEqual(numVariable.Identifier + " = 42", sut.ToString());
        }
    }
}
