using System.Collections.Generic;
using System.Linq;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies.msfScaling
{
    [TestFixture]
    public class ProdScaleStrategyTest
    {
        [Test]
        public void Apply_With_No_Point()
        {
            var msf = new MembershipFunction();

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Apply_With_One_Point()
        {
            var msf = new MembershipFunction{ { 0, 0.7 } };

            var sut = new ProdMsfScalingStrategy();

            var result1 = sut.Apply(msf, 0.5);

            Assert.AreEqual(1, result1.Count);
            foreach (var point in result1)
                Assert.AreEqual(0.5*0.7, point.Value);
        }

        [Test]
        public void Apply_With_Triangle()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 0 } };

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(3, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0)));
        }
        
        [Test]
        public void Apply_With_Inverse_Triangle()
        {
            var msf = new MembershipFunction{ { 0, 1 }, { 1, 0 }, { 2, 1 } };

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(3, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0.5)));
        }

        [Test]
        public void Apply_With_Trapezoid()
        {
            var msf = new MembershipFunction{ { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 0 } };

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

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

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(4, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(3, 0.5)));
        }

        [Test]
        public void Apply_With_Complex()
        {
            var msf = new MembershipFunction {{0, 0}, {1, 0.5}, {2, 0.5}, {3, 1}, {4, 0}};

            var sut = new ProdMsfScalingStrategy();

            var result = sut.Apply(msf, 0.5);

            Assert.AreEqual(5, result.Count);

            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(0, 0)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(1, 0.25)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(2, 0.25)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(3, 0.5)));
            Assert.IsTrue(result.Contains(new KeyValuePair<double, double>(4, 0)));
        }

        [Test]
        public void ToStringTest()
        {
            var sut = new ProdMsfScalingStrategy();

            Assert.AreEqual("Product Msf Scaling", sut.ToString());
        }
    }
}
