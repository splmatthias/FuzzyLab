using System.Collections.Generic;
using fuzzyController.inference.valueMerger.strategies;
using NUnit.Framework;

namespace fuzzyController.test.inference.valueMerger.strategies
{
    [TestFixture]
    public class MaxValueStrategyTest
    {
        [Test]
        public void Merge()
        {
            var sut = new MaxValueStrategy();

            var result = sut.Merge(new List<double> {0.2, 0.6});

            Assert.AreEqual(0.6, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MaxValueStrategy();

            Assert.AreEqual("Maximum Value", sut.ToString());
        }
    }
}
