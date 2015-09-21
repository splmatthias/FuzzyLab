using System.Collections.Generic;
using System.Linq;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies.msfScaling
{
    [TestFixture]
    public class MinScaleStrategyTest
    {
        [Test]
        public void Apply_With_No_Point()
        {
            var msf = new MembershipFunction();

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Apply_With_One_Point()
        {
            var msf = new MembershipFunction{{0, 0.7}};

            var sut = new MinMsfScalingStrategy();

            var result1 = sut.Apply(msf, 0.5);
            var result2 = sut.Apply(msf, 0.9);

            Assert.AreEqual(1, result1.Count);
            foreach (var point in result1)
                Assert.AreEqual(0.5, point.Value);

            Assert.AreEqual(1, result2.Count);
            foreach (var point in result2)
                Assert.AreEqual(0.7, point.Value);
        }


        [Test]
        public void Apply_With_Triangle()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0)));
        }

        [Test]
        public void Apply_With_Triangle_2()
        {
            var msf = new MembershipFunction { { -7, 0 }, { -5, 1 }, { 0, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.6);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(0, msf.Apply(-7), 0.000000000001);
            Assert.AreEqual(0.6, msf.Apply(-5.8), 0.000000000001);
            Assert.AreEqual(0.6, msf.Apply(-3), 0.000000000001);
            Assert.AreEqual(0, msf.Apply(0), 0.000000000001);
        }

        [Test]
        public void Apply_With_Triangle_3()
        {
            var msf = new MembershipFunction { { -7, 0 }, { -5, 1 }, { 0, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(0, msf.Apply(-7), 0.000000000001);
            Assert.AreEqual(0.5, msf.Apply(-6), 0.000000000001);
            Assert.AreEqual(0.5, msf.Apply(-2.5), 0.000000000001);
            Assert.AreEqual(0, msf.Apply(0), 0.000000000001);
        }

        [Test]
        public void Apply_With_Triangle_No_Effect()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 0.5 }, { 2, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 1);

            Assert.AreEqual(3, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0)));
        }

        [Test]
        public void Apply_With_Inverse_Triangle()
        {
            var msf = new MembershipFunction{ { 0, 1 }, { 1, 0 }, { 2, 1 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(3, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1.5, 0.5)));
        }

        [Test]
        public void Apply_With_Trapezoid()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(3, 0)));
        }

        [Test]
        public void Apply_With_Trapezoid_No_Effect()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 0.5 }, { 2, 0.5 }, { 3, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.7);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(3, 0)));
        }

        [Test]
        public void Apply_With_Inverse_Trapezoid()
        {
            var msf = new MembershipFunction{ { 0, 1 }, { 1, 0 }, { 2, 0 }, { 3, 1 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2.5, 0.5)));
        }
        
        [Test]
        public void Apply_With_Complex()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 0.5 }, { 2, 0.5 }, { 3, 1 }, { 4, 0 } };

            var sut = new MinMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(3.5, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(4, 0)));
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new MinMsfScalingStrategy();

            Assert.AreEqual("Minumum Msf Scaling", sut.ToString());
        }
    }
}
