using System;
using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies
{
    [TestFixture]
    public class MaximumDefuzzifyStrategyTest
    {
        private IDefuzzifyStrategy createSut(string method)
        {
            if (method == "Average")
                return new AverageMaximumStrategy();
            if (method == "Left")
                return new LeftMaximumStrategy();
            if (method == "Right")
                return new RightMaximumStrategy();

            throw new Exception("Unkown method");
        }

        [TestCase("Average", 1.5)]
        [TestCase("Left", 1)]
        [TestCase("Right", 2)]
        public void Apply_1( string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 1.5, 0.5 }, { 2, 1 }, { 3, 0 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 1)]
        [TestCase("Left", 1)]
        [TestCase("Right", 1)]
        public void Apply_2(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 3, 0 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 1.5)]
        [TestCase("Left", 1)]
        [TestCase("Right", 2)]
        public void Apply_3(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 0 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 1.5)]
        [TestCase("Left", 0)]
        [TestCase("Right", 3)]
        public void Apply_4(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 1, 1 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 2)]
        [TestCase("Left", 1)]
        [TestCase("Right", 3)]
        public void Apply_5(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 0.5)]
        [TestCase("Left", 0)]
        [TestCase("Right", 1)]
        public void Apply_6(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 3);
            var msf = new MembershipFunction { { 1, 1 }, {2, 0 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", 2.5)]
        [TestCase("Left", 2)]
        [TestCase("Right", 3)]
        public void Apply_7(string method, double expected)
        {
            var numVar = new NumericVariable("Num Variable", 0, 5);
            var msf = new MembershipFunction { { 1, 0.5 }, { 2, 1 }, { 3, 1 }, { 4, 0.5 } };

            var sut = createSut(method);

            var result = sut.Apply(numVar, msf);

            Assert.AreEqual(expected, result);
        }

        [TestCase("Average", "Average Maximum")]
        [TestCase("Left", "Left Maximum")]
        [TestCase("Right", "Right Maximum")]
        public void ToStringTest(string method, string expected)
        {
            var sut = createSut(method);

            var result = sut.ToString();

            Assert.AreEqual(expected, result);
        }
    }
}
