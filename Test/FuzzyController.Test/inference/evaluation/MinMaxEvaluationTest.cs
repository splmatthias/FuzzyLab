using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class MinMaxEvaluationTest
    {
        [Test]
        public void And()
        {
            var sut = new MinMaxEvaluation();

            var result = sut.And(0.2, 0.6);

            Assert.AreEqual(0.2, result);
        }

        [Test]
        public void Or()
        {
            var sut = new MinMaxEvaluation();

            var result = sut.Or(0.2, 0.6);

            Assert.AreEqual(0.6, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MinMaxEvaluation();

            Assert.AreEqual("MinMax Evaluation", sut.ToString());
        }
    }
}
