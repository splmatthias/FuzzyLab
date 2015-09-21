using System.Collections.Generic;
using fuzzyController.inference.valueMerger.strategies;
using NUnit.Framework;

namespace fuzzyController.test.inference.valueMerger.strategies
{
    [TestFixture]
    public class MinValueStrategyTest
    {
        [Test]
        public void Merge()
        {
            var sut = new MinValueStrategy();

            var result = sut.Merge(new List<double> {0.2, 0.6});

            Assert.AreEqual(0.2, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MinValueStrategy();

            Assert.AreEqual("Minimum Value", sut.ToString());
        }
    }
}
