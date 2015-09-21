using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class LukasiewiczEvaluationTest
    {
        [Test]
        public void And()
        {
            var sut = new LukasiewiczEvaluation();

            var result = sut.And(0.5, 0.7);

            Assert.AreEqual(0.2, result, 0.000000000001);
        }

        [Test]
        public void Or()
        {
            var sut = new LukasiewiczEvaluation();

            var result = sut.Or(0.2, 0.6);

            Assert.AreEqual(0.8, result, 0.000000000001);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new LukasiewiczEvaluation();

            Assert.AreEqual("Lukasiewicz Evaluation", sut.ToString());
        }
    }
}
