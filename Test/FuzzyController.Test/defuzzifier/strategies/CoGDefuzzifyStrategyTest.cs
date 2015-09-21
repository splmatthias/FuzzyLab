using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.defuzzifier.strategies
{
    [TestFixture]
    public class CoGDefuzzifyStrategyTest
    {
        [Test]
        public void Apply_On_Empty_Msf()
        {
            var var = new NumericVariable("Var", 0, 2);
            var msf = new MembershipFunction();

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 1d;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_1()
        {
            var var = new NumericVariable("Var", 0, 2);
            var msf = new MembershipFunction {{0, 0}, {1, 1}, {2, 0}};

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 1d;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_2()
        {
            var var = new NumericVariable("Var", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 0 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 1.5;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_3()
        {
            var var = new NumericVariable("Var", 0, 4);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 2, 0.5 }, { 3, 1 }, { 4, 0 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 2;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_4()
        {
            var var = new NumericVariable("Var", -5, 5);
            var msf = new MembershipFunction { { -5, 0 }, { -4, 1 }, { -3, 0 }, { -2, 1 }, { 2, 1 }, { 3, 0 }, { 4, 1 }, { 5, 0 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_5()
        {
            var var = new NumericVariable("Var", -5, 5);
            var msf = new MembershipFunction { { 1, 1 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_On_Symmetric_Msf_6()
        {
            var var = new NumericVariable("Var", -6, 6);
            var msf = new MembershipFunction { { -5, 0.5 }, { -4, 1 }, { -3, 0 }, { -2, 1 }, { 2, 1 }, { 3, 0 }, { 4, 1 }, { 5, 0.5 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_1()
        {
            var var = new NumericVariable("Var", 0, 1);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 2.0/3.0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_2()
        {
            var var = new NumericVariable("Var", 1, 2);
            var msf = new MembershipFunction { { 1, 0.5 }, { 2, 1 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 14.0 / 9.0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_3()
        {
            var var = new NumericVariable("Var", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 1 }, { 1.5, 0.5 }, { 2, 1 }, { 3, 0 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 1.5;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }

        [Test]
        public void Apply_4()
        {
            var var = new NumericVariable("Var", 0, 3);
            var msf = new MembershipFunction { { 0, 0 }, { 1, 0.5 }, { 3, 0.5 }, { 4, 1 }, { 6, 0 } };

            var sut = new CoGDefuzzifyStrategy();

            const double expectedResult = 57.0/18.0;

            var result = sut.Apply(var, msf);

            Assert.AreEqual(expectedResult, result, 0.00000000001);
        }
    }
}
