using System.Collections.Generic;
using fuzzyController.defuzzifier;
using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.variables;
using NUnit.Framework;
using Rhino.Mocks;

namespace fuzzyController.test.defuzzifier
{
    [TestFixture]
    public class DefuzzifierTest
    {
        [Test]
        public void Apply_Unit_Test()
        {
            var numVar = new NumericVariable("Num Variable", 0, 42);
            var msf1 = new MembershipFunction { { 1, 0 } };
            var msf2 = new MembershipFunction { { 2, 0 } };
            var term1 = new FuzzyTerm("Term1", msf1);
            var term2 = new FuzzyTerm("Term2", msf2);
            const double value1 = 0.3;
            const int value2 = 06;

            var fuzzyVariable = new FuzzyVariable("Var", numVar, term1, term2);
            var fuzzyValue = new FuzzyValue(fuzzyVariable,
                new Dictionary<FuzzyTerm, double> {{term1, value1}, {term2, value2}});
            var scaledMsf1 = new MembershipFunction {{0, 0}};
            var scaledMsf2 = new MembershipFunction {{1, 1}};

            var mergedValue = new MembershipFunction {{2, 1}};

            const double expectedDefuzzifiedValue = 42;

            var mocks = new MockRepository();

            var scaleStrategy = mocks.StrictMock<IMsfScalingStrategy>();
            var mergeStrategy = mocks.StrictMock<IMsfMergingStrategy>();
            var defuzzifyStrategy = mocks.StrictMock<IDefuzzifyStrategy>();

            // 1. Scale the membership functions according to the fuzzy value
            Expect.Call(scaleStrategy.Apply(msf1, value1)).Return(scaledMsf1).Repeat.Once();
            Expect.Call(scaleStrategy.Apply(msf2, value2)).Return(scaledMsf2).Repeat.Once();

            // 2. Merge all membership functions into single one
            Expect.Call(mergeStrategy.Apply(new[] {scaledMsf1, scaledMsf2})).IgnoreArguments().Return(mergedValue).Repeat.Once();

            // 3. Create a defuzzified value for the result of the previous merge.
            Expect.Call(defuzzifyStrategy.Apply(numVar, mergedValue)).Return(expectedDefuzzifiedValue).Repeat.Once();
            
            mocks.ReplayAll();

            var sut = new Defuzzifier(scaleStrategy, mergeStrategy, defuzzifyStrategy);

            var result = sut.Apply(fuzzyValue);

            Assert.AreEqual(fuzzyVariable, result.Variable);
            Assert.AreEqual(mergedValue, result.MembershipFunction);
            Assert.AreEqual(expectedDefuzzifiedValue, result.Value);

            mocks.VerifyAll();
        }
    }
}
