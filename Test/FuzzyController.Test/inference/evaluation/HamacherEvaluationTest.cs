using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class HamacherEvaluationTest
    {
        [Test]
        public void And()
        {
            var sut = new HamacherEvaluation();

            var a = 0.2;
            var b = 0.6;

            var result = sut.And(a, b);

            Assert.AreEqual((a*b)/(a + b - a*b), result);
        }

        [Test]
        public void Or()
        {
            var sut = new HamacherEvaluation();

            var a = 0.2;
            var b = 0.6;

            var result = sut.Or(a, b);

            Assert.AreEqual((a + b - 2*a*b)/(1 - a*b), result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new HamacherEvaluation();

            Assert.AreEqual("Hamacher Evaluation", sut.ToString());
        }
    }
}
