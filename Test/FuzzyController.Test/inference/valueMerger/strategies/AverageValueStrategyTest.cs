using System.Collections.Generic;
using fuzzyController.inference.valueMerger.strategies;
using NUnit.Framework;

namespace fuzzyController.test.inference.valueMerger.strategies
{
    [TestFixture]
    public class AverageValueStrategyTest
    {
        [Test]
        public void Merge()
        {
            var sut = new AverageValueStrategy();

            var result = sut.Merge(new List<double> {0.2, 0.6});

            Assert.AreEqual(0.4, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new AverageValueStrategy();

            Assert.AreEqual("Average Value", sut.ToString());
        }
    }
}
