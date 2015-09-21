using System;
using fuzzyController.math;
using NUnit.Framework;

namespace fuzzyController.test.math
{
    [TestFixture]
    public class LineSegmentTest
    {
        [Test]
        public void Constructor()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(2, 2));

            Assert.AreEqual(new Point(0, 0), sut.Start);
            Assert.AreEqual(new Point(2, 2), sut.End);
            Assert.AreEqual(1 , sut.Gradient);
        }

        [Test]
        public void Constructor_Fails()
        {
            Assert.Throws<ArgumentException>(() => new LineSegment(new Point(2, 2), new Point(0, 0)));
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(2, 2));

            Assert.AreEqual("(0;0) -> (2;2)", sut.ToString());
        }

        [Test]
        public void Contains_Point()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(2, 2));

            Assert.IsTrue(sut.Contains(new Point(0, 0)));
            Assert.IsTrue(sut.Contains(new Point(1, 1)));
            Assert.IsTrue(sut.Contains(new Point(2, 2)));
        }
        [Test]
        public void Does_Not_Contain_Point()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(2, 2));

            Assert.IsFalse(sut.Contains(new Point(-1, -1)));
            Assert.IsFalse(sut.Contains(new Point(3, 3)));
            Assert.IsFalse(sut.Contains(new Point(3, 0)));
            Assert.IsFalse(sut.Contains(new Point(1, 2)));
        }

        /// <summary>
        /// Lines intersect in between their start and end points.
        /// </summary>
        [Test]
        public void Intersects_1()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(0, 4), new Point(4, 0)));
            Assert.AreEqual(new Point(2, 2), result);
        }

        /// <summary>
        /// Second line starts at end point of first line.
        /// </summary>
        [Test]
        public void Intersects_2a()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(4, 4), new Point(6, 0)));
            Assert.AreEqual(new Point(4, 4), result);
        }

        /// <summary>
        /// Second line ends at start point of first line.
        /// </summary>
        [Test]
        public void Intersects_2b()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(-2, 4), new Point(0, 0)));
            Assert.AreEqual(new Point(0, 0), result);
        }

        /// <summary>
        /// Starting point of second line lies on first.
        /// </summary>
        [Test]
        public void Intersects_3()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(3, 3), new Point(6, 0)));
            Assert.AreEqual(new Point(3, 3), result);
        }

        /// <summary>
        /// Line intersects with horizontal line
        /// </summary>
        [Test]
        public void Intersects_4()
        {
            var line1 = new LineSegment(new Point(0, 0), new Point(4, 4));
            var line2 = new LineSegment(new Point(0, 3), new Point(6, 3));

            Assert.AreEqual(new Point(3, 3), line1.Intersect(line2));
            Assert.AreEqual(new Point(3, 3), line2.Intersect(line1));
        }

        /// <summary>
        /// Line intersects with vertical line.
        /// </summary>
        [Test]
        public void Intersects_5()
        {
            var line1 = new LineSegment(new Point(0, 0), new Point(4, 4));
            var line2 = new LineSegment(new Point(3, 0), new Point(3, 6));

            Assert.AreEqual(new Point(3, 3), line1.Intersect(line2));
            Assert.AreEqual(new Point(3, 3), line2.Intersect(line1));
        }

        /// <summary>
        /// Horizonal and vertical lines intersect.
        /// </summary>
        [Test]
        public void Intersects_6()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(0, 4));
            var result = sut.Intersect(new LineSegment(new Point(-2, 2), new Point(2, 2)));
            Assert.AreEqual(new Point(0, 2), result);
        }

        [Test]
        public void Intersects_7()
        {
            var sut = new LineSegment(new Point(3, 0.5), new Point(5, 0.5));

            var result = sut.Intersect(new LineSegment(new Point(3, 0.5), new Point(4, 1)));
            Assert.AreEqual(new Point(3, 0.5), result);
        }
        
        /// <summary>
        /// Lines overlap.
        /// </summary>
        [Test]
        public void Dont_Intersects_1()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(2, 2), new Point(5, 5)));
            Assert.IsNull(result);
        }

        /// <summary>
        /// Lines don't intersect.
        /// </summary>
        [Test]
        public void Dont_Intersects_2()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(2, 1), new Point(6, 0)));
            Assert.IsNull(result);
        }

        /// <summary>
        /// Parallel lines
        /// </summary>
        [Test]
        public void Dont_Intersects_3()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(1, 0), new Point(5, 4)));
            Assert.IsNull(result);
        }

        /// <summary>
        /// Parallel vertical lines
        /// </summary>
        [Test]
        public void Dont_Intersects_4()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(0, 4));

            var result = sut.Intersect(new LineSegment(new Point(1, 0), new Point(1, 4)));
            Assert.IsNull(result);
        }

        [Test]
        public void Dont_Intersects_5()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(0, 5), new Point(5, 5)));
            Assert.IsNull(result);
        }

        [Test]
        public void Dont_Intersects_6()
        {
            var sut = new LineSegment(new Point(0, 0), new Point(4, 4));

            var result = sut.Intersect(new LineSegment(new Point(5, 0), new Point(5, 5)));
            Assert.IsNull(result);
        }
    }
}
