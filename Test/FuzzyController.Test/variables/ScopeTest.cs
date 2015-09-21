using System;
using System.Collections;
using System.Collections.Generic;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.variables
{
    [TestFixture]
    public class ScopeTest
    {
        [Test]
        public void Index_Test()
        {
            runTest((sut, value1, value2) =>
            {
                Assert.AreEqual(value1, sut[0]);
                Assert.AreEqual(value2, sut[1]);
            });
        }

        [Test]
        public void Count_Test()
        {
            runTest((sut, value1, value2) => Assert.AreEqual(2, sut.Count));
        }

        [Test]
        public void IEnumerable_Interface_Test()
        {
            runTest((sut, value1, value2) =>
            {
                var enumerator = ((IEnumerable) sut).GetEnumerator();

                enumerator.MoveNext();
                Assert.AreEqual(value1, enumerator.Current);
                enumerator.MoveNext();
                Assert.AreEqual(value2, enumerator.Current);
                Assert.IsFalse(enumerator.MoveNext());
            });
        }

        [Test]
        public void Generic_IEnumerable_Interface_Test()
        {
            runTest((sut, value1, value2) =>
            {
                var enumerator = ((IEnumerable<FuzzyValue>) sut).GetEnumerator();

                enumerator.MoveNext();
                Assert.AreEqual(value1, enumerator.Current);
                enumerator.MoveNext();
                Assert.AreEqual(value2, enumerator.Current);
                Assert.IsFalse(enumerator.MoveNext());
            });
        }

        private void runTest(Action<Scope, FuzzyValue, FuzzyValue> runAndAssert)
        {
            var num1 = new NumericVariable("Variable1");
            var num2 = new NumericVariable("Variable2");
            var term11 = new FuzzyTerm("Var1_Term1", new MembershipFunction());
            var term12 = new FuzzyTerm("Var1_Term2", new MembershipFunction());
            var term21 = new FuzzyTerm("Var2_Term1", new MembershipFunction());
            var term22 = new FuzzyTerm("Var2_Term2", new MembershipFunction());
            var var1 = new FuzzyVariable("Variable1", num1, term11, term12);
            var var2 = new FuzzyVariable("Variable2", num2, term21, term22);
            var value1 = new FuzzyValue(var1, new Dictionary<FuzzyTerm, double> {{term11, 0.4}, {term12, 0.3}});
            var value2 = new FuzzyValue(var2, new Dictionary<FuzzyTerm, double> {{term21, 0.1}, {term22, 0.2}});
            var sut = new Scope(value1, value2);

            runAndAssert(sut, value1, value2);
        }
    }
}
