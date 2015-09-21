using System.Linq;
using fuzzyController.fuzzifier;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.fuzzifier
{
    [TestFixture]
    public class FuzzifierTest
    {
        [Test]
        public void EmptyFuzzifier()
        {
            var sut = new Fuzzifier();

            Assert.IsFalse(sut.Apply().Any());
            Assert.IsFalse(sut.Apply(new NumericValue(new NumericVariable("Test"), 42 )).Any());
        }

        [Test]
        public void Test()
        {
            var nVar = new NumericVariable("Km/h", 0);
            var slow = new FuzzyTerm("Slow", new MembershipFunction{ { 30, 1 }, { 50, 0 } });
            var medium = new FuzzyTerm("Medium", new MembershipFunction{ { 40, 0 }, { 70, 1 }, { 110, 0 } });
            var fast = new FuzzyTerm("Fast", new MembershipFunction{ { 70, 0 }, { 90, 1 } });
            var speed = new FuzzyVariable("Speed", nVar, slow, medium, fast);

            
            var sut = new Fuzzifier(speed);

            var result = sut.Apply(new NumericValue(nVar, 90));

            Assert.AreEqual(1, result.Count);
            var speedValue = result[0];

            Assert.AreEqual(speed, speedValue.AssociatedVariable);
            Assert.AreEqual(2, speedValue.Values.Count);
            Assert.AreEqual(0.5, speedValue.Values[medium]);
            Assert.AreEqual(1, speedValue.Values[fast]);
        }
    }
}
