using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies.msfMerging
{
    [TestFixture]
    [Category("Merging Strategies")]
    public class MaxMsfMergingStrategyTest
    {
        [Test]
        public void Merge_1()
        {
            var msf1 = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction{ { 1, 0 }, { 2, 1 }, { 3, 0 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 1.5, 0.5 }, { 2, 1 }, { 3, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_2()
        {
            var msf1 = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction{ { 2, 0 }, { 3, 1 }, { 4, 0 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 }, { 3, 1 }, { 4, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_3()
        {
            var msf1 = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };
            var msf2 = new MembershipFunction{ { 3, 0 }, { 4, 1 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 }, { 3, 0 }, { 4, 1 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_4()
        {
            var msf1 = new MembershipFunction{ { 0, 0 }, { 1, 0.5 }, { 5, 0.5 }, { 6, 0 } };
            var msf2 = new MembershipFunction{ { 2, 0 }, { 4, 1 }, { 6, 0 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 1, 0.5 }, { 3, 0.5 }, { 4, 1 }, { 6, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_5()
        {
            var msf1 = new MembershipFunction{ { 1, 0 }, { 2, 1 }, { 3, 0 } };
            var msf2 = new MembershipFunction{ { 0, 0 }, { 2, 1 }, { 4, 0 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 2, 1 }, { 4, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_6()
        {
            var msf1 = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };

            var expected = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1 });

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_7()
        {
            var expected = new MembershipFunction();

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new MembershipFunction[] {});

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_8()
        {
            var msf1 = new MembershipFunction();
            var msf2 = new MembershipFunction { { 0, 0 }, { 2, 1 }, { 4, 0 } };

            var expected = new MembershipFunction { { 0, 0 }, { 2, 1 }, { 4, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });
            Assert.AreEqual(expected, result);
            result = sut.Apply(new[] { msf2, msf1 });
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Merge_9()
        {
            var msf1 = new MembershipFunction { { -1, 1 }, { 0, 0 }};
            var msf2 = new MembershipFunction { { -1, 0 }, { -0.75, 0.25 }, { 0.75, 0.25 }, { 1, 0 } };

            var expected = new MembershipFunction { { -1, 1 }, { -0.25, 0.25 }, { 0.75, 0.25 }, { 1, 0 } };

            var sut = new MaxMsfMergingStrategy();
            var result = sut.Apply(new[] { msf1, msf2 });
            Assert.AreEqual(expected, result);
            result = sut.Apply(new[] { msf2, msf1 });
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MaxMsfMergingStrategy();

            Assert.AreEqual("Maximum Msf Merge", sut.ToString());
        }
    }
}
