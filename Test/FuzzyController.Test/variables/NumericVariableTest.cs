using System;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class NumericVariableTest
    {
        [Test]
        public void Constructor()
        {
            var sut = new NumericVariable("MyIdentifier", -42, 1701);
            
            Assert.AreEqual("MyIdentifier", sut.Identifier);
            Assert.AreEqual(-42, sut.MinValue);
            Assert.AreEqual(1701, sut.MaxValue);
        }

        [Test]
        public void ConstructorFail()
        {
            Assert.AreEqual("identifier", Assert.Throws<ArgumentException>(() => new NumericVariable("")).Message);

            Assert.AreEqual("maxValue", Assert.Throws<ArgumentException>(() => new NumericVariable("MyIdentifier", 42, 13)).Message);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new NumericVariable("MyIdentifier");

            Assert.AreEqual(sut.Identifier, sut.ToString());
        }

        [Test]
        public void GetHashCodeTest()
        {
            var sut = new NumericVariable("MyIdentifier");

            Assert.AreEqual(sut.Identifier.GetHashCode(), sut.GetHashCode());
        }

        [Test]
        public void EqualsTest()
        {
            var sut2 = new NumericVariable("MyIdentifier");
            var sut1 = new NumericVariable("MyIdentifier");

// ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(sut1.Equals("MyIdentifier"));
// ReSharper restore SuspiciousTypeConversion.Global
            Assert.IsTrue(sut1.Equals(sut2));
        }
    }
}
