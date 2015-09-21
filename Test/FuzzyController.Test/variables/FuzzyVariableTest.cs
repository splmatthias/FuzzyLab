using System;
using System.Linq;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class FuzzyVariableTest
    {
        [Test]
        public void Constructor()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm = new FuzzyTerm("MyFuzzyTerm",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var sut = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm);

            Assert.AreEqual("MyFuzzyVariable", sut.Identifier);
            Assert.AreEqual(numVariable, sut.NumericVariable);
            Assert.AreEqual(1, sut.FuzzyTerms.Count());
            Assert.AreEqual(fuzzyTerm, sut.FuzzyTerms.ElementAt(0));
        }


        [Test]
        public void ConstructorFail()
        {
            Assert.AreEqual("identifier", Assert.Throws<ArgumentException>(() => new FuzzyVariable("", null)).Message);
        }

        [Test]
        public void ToStringTest()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm1 = new FuzzyTerm("FuzzyTerm1",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });
            var fuzzyTerm2 = new FuzzyTerm("FuzzyTerm2",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var sut = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm1, fuzzyTerm2);

            Assert.AreEqual("MyFuzzyVariable = { FuzzyTerm1, FuzzyTerm2 }", sut.ToString());
        }

        [Test]
        public void GetHashCodeTest()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm = new FuzzyTerm("FuzzyTerm",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var sut = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm);

            Assert.AreEqual(sut.Identifier.GetHashCode(), sut.GetHashCode());
        }

        [Test]
        public void EqualsTest()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm1 = new FuzzyTerm("FuzzyTerm1",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });
            var fuzzyTerm2 = new FuzzyTerm("FuzzyTerm2",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var sut1 = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm1);
            var sut2 = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm2);

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(sut1.Equals("MyIdentifier"));
            // ReSharper restore SuspiciousTypeConversion.Global
            Assert.IsTrue(sut1.Equals(sut2));
        }
    }
}
