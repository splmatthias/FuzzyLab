using fuzzyController.math;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class MembershipFunctionTest
    {
        [Test]
        public void NoPointsMeansAlwaysZero()
        {
            var sut = new MembershipFunction();

            Assert.AreEqual(0, sut.Apply(42.0));
        }
        
        [Test]
        public void ApplyWithOnePoint()
        {
            var sut = new MembershipFunction{ { 42, 1 } };

            Assert.AreEqual(1, sut.Apply(42.0));
            // The membership value of the one point exists everywhere
            Assert.AreEqual(1, sut.Apply(0));
            Assert.AreEqual(1, sut.Apply(1701));
        }

        [Test]
        public void ApplyForTriangle()
        {
            var sut = new MembershipFunction {{1, 0}, {3, 1}, {5, 0}};

            Assert.AreEqual(0.0, sut.Apply(0));
            Assert.AreEqual(0.0, sut.Apply(1));
            Assert.AreEqual(0.5, sut.Apply(2));
            Assert.AreEqual(1.0, sut.Apply(3));
            Assert.AreEqual(0.5, sut.Apply(4));
            Assert.AreEqual(0.0, sut.Apply(5));
            Assert.AreEqual(0.0, sut.Apply(6));
        }

        [Test]
        public void ApplyForComplex()
        {
            var sut = new MembershipFunction { { 0, 0.5 }, { 1, 0 }, { 2, 0.5 }, { 3, 0.5 }, { 5, 1 }, { 6, 0.5 } };

            Assert.AreEqual( 0.5, sut.Apply(-1));
            Assert.AreEqual( 0.5, sut.Apply(0));
            Assert.AreEqual(0.25, sut.Apply(1.5));
            Assert.AreEqual( 0.5, sut.Apply(2));
            Assert.AreEqual( 0.5, sut.Apply(3));
            Assert.AreEqual(0.75, sut.Apply(4));
            Assert.AreEqual(   1, sut.Apply(5));
            Assert.AreEqual( 0.5, sut.Apply(6));
            Assert.AreEqual( 0.5, sut.Apply(7));
        }



        [Test]
        public void ClearUp_1()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0 } },
                new MembershipFunction { { -2, 0 }, { 0, 0 }, { 0.5, 0.5 }, { 1, 1 }, { 2, 0 }, { 3, 0 } }.ClearUp());
        }

        [Test]
        public void ClearUp_2()
        {
            Assert.AreEqual(
                new MembershipFunction {{0, 0}, {1, 1}, {3, 1}, {4, 0}},
                new MembershipFunction {{0, 0}, {1, 1}, {2, 1}, {3, 1}, {4, 0}}.ClearUp());
        }

        [Test]
        public void ClearUp_3()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0 }, { 1, 1 }, { 3, 1 }, { 4, 0 } },
                new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 0 } }.ClearUp());
        }

        [Test]
        public void ClearUp_4()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 3, 0.5 }, { 4, 1 }, { 6, 0 } },
                new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 2, 0.5 }, { 3, 0.5 }, { 4, 1 }, { 5, 0.5 }, { 6, 0 } }.ClearUp());
        }

        [Test]
        public void ClearUp_5()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 3.5, 0.5 }, { 4, 0 } },
                new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 2, 0.5 }, { 3, 0.5 }, { 3.5, 0.5 }, { 4, 0 } }.ClearUp());
        }

        [Test]
        public void ClearUp_6()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0.5 } },
                new MembershipFunction { { 0, 0.5 }, { 1, 0.5 } }.ClearUp());
        }

        [Test]
        public void ClearUp_7()
        {
            Assert.AreEqual(
                new MembershipFunction { { 0, 0.5 }, { 1, 0.7 } },
                new MembershipFunction { { 0, 0.5 }, { 1, 0.7 } }.ClearUp());
        }

        [Test]
        public void Index_Int()
        {
            var sut = new MembershipFunction { { 1, 0 }, { 3, 1 }, { 5, 0 } };

            Assert.AreEqual(new Point(1, 0), sut[0]);
            Assert.AreEqual(new Point(3, 1), sut[1]);
            Assert.AreEqual(new Point(5, 0), sut[2]);
        }

        [Test]
        public void EqualTest()
        {
            var obj1 = new MembershipFunction { { 1, 0 }, { 3, 1 }, { 5, 0 } };
            var obj2 = new MembershipFunction { { 1, 0 }, { 2, 1 }};

            var sut = new MembershipFunction { { 1, 0 }, { 3, 1 }, { 5, 0 } };

            Assert.IsFalse(sut.Equals(null as object));
            Assert.IsTrue(sut.Equals(sut as object));
            Assert.IsTrue(sut.Equals(obj1 as object));
            Assert.IsFalse(sut.Equals(obj2));
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MembershipFunction { { 1, 0 }, { 3, 1 }, { 5, 0 } };

            Assert.AreEqual("{ (1;0) | (3;1) | (5;0) }", sut.ToString());
        }
    }
}
