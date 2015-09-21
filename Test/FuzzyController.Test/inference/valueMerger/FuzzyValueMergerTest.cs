using System;
using System.Collections.Generic;
using System.Linq;
using fuzzyController.inference.valueMerger;
using fuzzyController.inference.valueMerger.strategies;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.test.inference.valueMerger
{
    [TestFixture]
    public class FuzzyValueMergerTest
    {
        private static IMergingStrategy createMergingStrategy(string strategyName)
        {
            switch (strategyName)
            {
                case "Max":
                    return new MaxValueStrategy();
                case "Min":
                    return new MinValueStrategy();
                case "Sum":
                    return new SumValueStrategy();
                case "Average":
                    return new AverageValueStrategy();
            }
            throw new ArgumentException("Unknown strategy name");
        }

        [Test]
        public void Apply([Values("Max", "Min", "Sum", "Average")] string strategyName)
        {
            var strategy = createMergingStrategy(strategyName);

            var term1 = new FuzzyTerm("Term1", new MembershipFunction());
            var term2 = new FuzzyTerm("Term2", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, term1, term2);

            var value1 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { term1, 0.2 } });
            var value2 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { term2, 0.6 } });

            var sut = new FuzzyValueMerger(strategy);
            
            var result = sut.Apply(new List<FuzzyValue> {value1, value2});

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(varA, result[0].AssociatedVariable);
            Assert.AreEqual(2, result[0].Values.Count);
            Assert.IsTrue(result[0].Values.ContainsKey(term1));
            Assert.IsTrue(result[0].Values.ContainsKey(term2));
            Assert.AreEqual(value1.Values[term1], result[0].Values[term1]);
            Assert.AreEqual(value2.Values[term2], result[0].Values[term2]);
        }

        [Test]
        public void Apply_With_Merging_of_Values([Values("Max", "Min", "Sum", "Average")] string strategyName)
        {
            var strategy = createMergingStrategy(strategyName);

            var term1 = new FuzzyTerm("Term1", new MembershipFunction());
            var term2 = new FuzzyTerm("Term2", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, term1, term2);

            var value1 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { term1, 0.6 } });
            var value2 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { term1, 0.8 }, { term2, 0.6 } });

            var sut = new FuzzyValueMerger(strategy);

            var result = sut.Apply(new List<FuzzyValue> { value1, value2 });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(varA, result[0].AssociatedVariable);
            Assert.AreEqual(2, result[0].Values.Count);
            Assert.IsTrue(result[0].Values.ContainsKey(term1));
            Assert.IsTrue(result[0].Values.ContainsKey(term2));
            Assert.AreEqual(strategy.Merge(new List<double>{value1.Values[term1], value2.Values[term1]}), result[0].Values[term1]);
            Assert.AreEqual(value2.Values[term2], result[0].Values[term2]);
        }

        [Test]
        public void Apply_Complex_Scenario([Values("Max", "Min", "Sum", "Average")] string strategyName)
        {
            var strategy = createMergingStrategy(strategyName);

            var termA1 = new FuzzyTerm("TermA1", new MembershipFunction());
            var termA2 = new FuzzyTerm("TermA2", new MembershipFunction());
            var varA = new FuzzyVariable("Variable A", null, termA1, termA2);

            var termB1 = new FuzzyTerm("TermB1", new MembershipFunction());
            var termB2 = new FuzzyTerm("TermB2", new MembershipFunction());
            var termB3 = new FuzzyTerm("TermB3", new MembershipFunction());
            var varB = new FuzzyVariable("Variable B", null, termB1, termB2, termB3);

            var valueA1 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA1, 0.6 } });
            var valueA2 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA2, 0.1 } });
            var valueA3 = new FuzzyValue(varA, new Dictionary<FuzzyTerm, double> { { termA1, 0.8 }, { termA2, 0.6 } });

            var valueB1 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB1, 0.2 } });
            var valueB2 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB2, 0.3 } });
            var valueB3 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB1, 0.8 }, { termB3, 0.6 } });
            var valueB4 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB3, 0.7 } });
            var valueB5 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB2, 0.8 }, { termB3, 0.6 } });
            var valueB6 = new FuzzyValue(varB, new Dictionary<FuzzyTerm, double> { { termB1, 0.3 }, { termB2, 0.5 }, { termB3, 0.7 } });

            var sut = new FuzzyValueMerger(strategy);

            var result = sut.Apply(new List<FuzzyValue> { valueA1, valueA2, valueA3, valueB1, valueB2, valueB3, valueB4, valueB5, valueB6 }).ToList();


            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Exists(v => v.AssociatedVariable.Equals(varA)));
            Assert.IsTrue(result.Exists(v => v.AssociatedVariable.Equals(varB)));

            // check for varA
            var varAValues = result.Find(v => v.AssociatedVariable.Equals(varA));
            Assert.AreEqual(2, varAValues.Values.Count);
            Assert.IsTrue(varAValues.Values.ContainsKey(termA1));
            Assert.IsTrue(varAValues.Values.ContainsKey(termA2));
            Assert.AreEqual(strategy.Merge(new List<double> { valueA1.Values[termA1], valueA3.Values[termA1] }), varAValues.Values[termA1]);
            Assert.AreEqual(strategy.Merge(new List<double> { valueA2.Values[termA2], valueA3.Values[termA2] }), varAValues.Values[termA2]);

            // check for varB
            var varBValues = result.Find(v => v.AssociatedVariable.Equals(varB));
            Assert.AreEqual(3, varBValues.Values.Count);
            Assert.IsTrue(varBValues.Values.ContainsKey(termB1));
            Assert.IsTrue(varBValues.Values.ContainsKey(termB2));
            Assert.IsTrue(varBValues.Values.ContainsKey(termB3));
            Assert.AreEqual(strategy.Merge(new List<double> { valueB1.Values[termB1], valueB3.Values[termB1], valueB6.Values[termB1] }), varBValues.Values[termB1]);
            Assert.AreEqual(strategy.Merge(new List<double> { valueB2.Values[termB2], valueB5.Values[termB2], valueB6.Values[termB2] }), varBValues.Values[termB2]);
            Assert.AreEqual(strategy.Merge(new List<double> { valueB3.Values[termB3], valueB4.Values[termB3], valueB5.Values[termB3], valueB6.Values[termB3] }), varBValues.Values[termB3]);
        }
    }
}
