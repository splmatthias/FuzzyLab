using System.Collections.Generic;
using fuzzyController.inference.valueMerger.strategies;
using NUnit.Framework;

namespace fuzzyController.test.inference.valueMerger.strategies
{
    [TestFixture]
    public class SumValueStrategyTest
    {
        [Test]
        public void Merge()
        {
            var sut = new SumValueStrategy();

            var result = sut.Merge(new List<double> {0.2, 0.6});

            Assert.AreEqual(0.8, result);
        }

        [Test]
        public void Merge_With_Sum_Greater_Than_One()
        {
            var sut = new SumValueStrategy();

            var result = sut.Merge(new List<double> { 0.6, 0.8 });

            // sum allows values greater than 1.0
            Assert.AreEqual(1.4, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new SumValueStrategy();

            Assert.AreEqual("Sum Value", sut.ToString());
        }
    }
}
