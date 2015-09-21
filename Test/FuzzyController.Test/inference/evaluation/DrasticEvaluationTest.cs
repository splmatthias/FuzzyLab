using fuzzyController.inference.evaluation;
using NUnit.Framework;

namespace fuzzyController.test.inference.evaluation
{
    [TestFixture]
    public class DrasticEvaluationTest
    {
        [Test]
        public void And_When_Max_Equals_One()
        {
            var sut = new DrasticEvaluation();

            var result = sut.And(0.2, 1.0);

            Assert.AreEqual(0.2, result);
        }

        [Test]
        public void And_When_Max_Does_Not_Equal_One()
        {
            var sut = new DrasticEvaluation();

            var result = sut.And(0.2, 0.8);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Or_When_Min_Equals_Zero()
        {
            var sut = new DrasticEvaluation();

            var result = sut.Or(0.0, 0.7);

            Assert.AreEqual(0.7, result);
        }

        [Test]
        public void Or_When_Min_Does_Not_Equal_Zero()
        {
            var sut = new DrasticEvaluation();

            var result = sut.Or(0.1, 0.7);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new DrasticEvaluation();

            Assert.AreEqual("Drastic Evaluation", sut.ToString());
        }
    }
}
