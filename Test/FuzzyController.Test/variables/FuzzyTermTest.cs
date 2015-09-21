using System;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class FuzzyTermTest
    {
        [Test]
        public void Constructor()
        {
            var membershipFunction = new MembershipFunction();

            var sut = new FuzzyTerm("MyFuzzyTerm", membershipFunction);

            Assert.AreEqual("MyFuzzyTerm", sut.Term);
            Assert.AreEqual(membershipFunction, sut.MembershipFunction);
        }

        [Test]
        public void ConstructorFails()
        {
            var membershipFunction = new MembershipFunction();

            Assert.AreEqual("term",
                Assert.Throws<ArgumentException>(() => new FuzzyTerm("", membershipFunction)).Message);
            Assert.AreEqual("membershipFunction",
                Assert.Throws<ArgumentNullException>(() => new FuzzyTerm("MyFuzzyTerm", null)).ParamName);
        }

        [Test]
        public void ToStringTest()
        {
            var membershipFunction = new MembershipFunction();

            var sut = new FuzzyTerm("MyFuzzyTerm", membershipFunction);

            Assert.AreEqual("MyFuzzyTerm", sut.ToString());
        }
    }
}
