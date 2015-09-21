using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies.msfMerging
{
    [TestFixture]
    [Category("Merging Strategies")]
    public class SumMsfMergingStrategyTest
    {
        [Test]
        public void Apply_1()
        {
            var msf1 = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction { { 1, 0 }, { 2, 1 }, { 3, 0 } };

            var expected = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 0 } };

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Apply_2()
        {
            var msf1 = new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 3, 0.5 }, { 4, 0 } };
            var msf2 = new MembershipFunction { { 1, 0 }, { 2, 0.5 }, { 3, 0 } };

            var expected = new MembershipFunction { { 0, 0 }, { 2, 1 }, { 4, 0 } };

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Apply_3()
        {
            var msf1 = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction { { 2, 0 }, { 3, 1 }, { 4, 0 } };

            var expected = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 }, { 3, 1 }, { 4, 0 } };

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Apply_4()
        {
            var msf1 = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 } };

            var expected = new MembershipFunction { { 0, 0 }, { 1, 2 }, { 2, 0 } };

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Apply_5()
        {
            var msf1 = new MembershipFunction { { 1, 0.5 } };
            var msf2 = new MembershipFunction { { 1, 1 } };

            var expected = new MembershipFunction { { 1, 1.5 } };

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_6()
        {
            var expected = new MembershipFunction();

            var sut = new SumMsfMergingStrategy();
            var result = sut.Apply(new MembershipFunction[] { });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_7()
        {
            var msf1 = new MembershipFunction { { 1, 1 } };
            var msf2 = new MembershipFunction();

            var sut = new SumMsfMergingStrategy();

            var result = sut.Apply(new [] { msf1, msf2 });
            Assert.AreEqual(msf1, result);

            result = sut.Apply(new[] { msf2, msf1 });
            Assert.AreEqual(msf1, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new SumMsfMergingStrategy();

            Assert.AreEqual("Sum Msf Merge", sut.ToString());
        }
    }
}
