using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class EinsteinEvaluationTest
    {
        [Test]
        public void And()
        {
            var sut = new EinsteinEvaluation();

            const double a = 0.2;
            const double b = 0.6;

            var result = sut.And(a, b);

            Assert.AreEqual((a*b)/(2 - (a + b - a*b)), result);
        }

        [Test]
        public void Or()
        {
            var sut = new EinsteinEvaluation();

            const double a = 0.2;
            const double b = 0.6;

            var result = sut.Or(a, b);

            Assert.AreEqual((a + b)/(1 + a*b), result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new EinsteinEvaluation();

            Assert.AreEqual("Einstein Evaluation", sut.ToString());
        }
    }
}
