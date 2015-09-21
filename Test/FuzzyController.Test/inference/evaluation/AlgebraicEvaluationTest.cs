using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class AlgebraicEvaluationTest
    {
        [Test]
        public void And()
        {
            var sut = new AlgebraicEvaluation();

            var result = sut.And(0.4, 0.6);

            Assert.AreEqual(0.4 * 0.6, result);
        }

        [Test]
        public void Or()
        {
            var sut = new AlgebraicEvaluation();

            var result = sut.Or(0.4, 0.6);

            Assert.AreEqual(0.4 + 0.6 - 0.4 * 0.6, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new AlgebraicEvaluation();

            Assert.AreEqual("Algebraic Evaluation", sut.ToString());
        }
    }
}
