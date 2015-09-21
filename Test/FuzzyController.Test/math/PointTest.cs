using fuzzyController.math;
using NUnit.Framework;

namespace fuzzyController.test.math
{
    [TestFixture]
    public class PointTest
    {
        [Test]
        public void Constructor()
        {
            var sut = new Point(4711, 42);

            Assert.AreEqual(4711, sut.X);
            Assert.AreEqual(42, sut.Y);
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new Point(1, 2);

            Assert.AreEqual("(1;2)", sut.ToString());
        }

        [Test]
        public void Equality()
        {
            var obj1 = new Point(1, 2);
            var obj2 = new Point(1, 1);

            var sut = new Point(1, 2);

            Assert.IsFalse(sut.Equals(null as object));
            Assert.IsTrue(sut.Equals(sut as object));
            Assert.IsTrue(sut.Equals(obj1));
            Assert.IsFalse(sut.Equals(obj2));
        }

        [Test]
        public void HashCodeTest()
        {
            var sut = new Point(3, 4);

            Assert.AreEqual((3d.GetHashCode()*397) ^ 4d.GetHashCode(), sut.GetHashCode());
        }
    }
}
