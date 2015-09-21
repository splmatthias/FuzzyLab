using System;
using System.Collections.Generic;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class FuzzyValueTest
    {
        [Test]
        public void Constructor()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm1 = new FuzzyTerm("FuzzyTerm1",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });
            var fuzzyTerm2 = new FuzzyTerm("FuzzyTerm2",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var fuzVariable = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm1, fuzzyTerm2);

            var sut = new FuzzyValue(fuzVariable,
                new Dictionary<FuzzyTerm, double>
            {
                {fuzzyTerm1, 1},
                {fuzzyTerm2, 0.5}
            });

            Assert.AreEqual(fuzVariable, sut.AssociatedVariable);
            Assert.AreEqual(2, sut.Values.Count);
            Assert.AreEqual(1, sut.Values[fuzzyTerm1]);
            Assert.AreEqual(0.5, sut.Values[fuzzyTerm2]);
        }

        [Test]
        public void ConstructorFails()
        {
            Assert.AreEqual("associatedVariable",
                Assert.Throws<ArgumentNullException>(() => new FuzzyValue(null, new Dictionary<FuzzyTerm, double>())).ParamName);
        }

        [Test]
        public void ToStringTest()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm1 = new FuzzyTerm("FuzzyTerm1",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });
            var fuzzyTerm2 = new FuzzyTerm("FuzzyTerm2",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var fuzVariable = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm1, fuzzyTerm2);

            var sut = new FuzzyValue(fuzVariable, 
                new Dictionary<FuzzyTerm, double>
            {
                {fuzzyTerm1, 1},
                {fuzzyTerm2, 0.5}
            });

            Assert.AreEqual(fuzVariable.Identifier + " = ( FuzzyTerm1=1, FuzzyTerm2=0,5 )", sut.ToString());
        }

        [Test]
        public void GetHashCodeTest()
        {
            var numVariable = new NumericVariable("MyNumVariable");
            var fuzzyTerm1 = new FuzzyTerm("FuzzyTerm1",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });
            var fuzzyTerm2 = new FuzzyTerm("FuzzyTerm2",
                new MembershipFunction{ { -2, 0 }, { 2, 1 } });

            var fuzVariable = new FuzzyVariable("MyFuzzyVariable", numVariable, fuzzyTerm1, fuzzyTerm2);

            var sut = new FuzzyValue(fuzVariable,new Dictionary<FuzzyTerm, double>{{fuzzyTerm1, 1}});

            Assert.AreEqual(fuzVariable.GetHashCode(), sut.GetHashCode());
        }
    }
}
