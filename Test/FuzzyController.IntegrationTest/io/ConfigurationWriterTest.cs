using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyController.io;
using fuzzyController.variables;
using NUnit.Framework;

namespace fuzzyController.integrationTest.io
{
    [TestFixture]
    public class ConfigurationWriterTest
    {
        [Test]
        public void WriteTest()
        {
            var config = new FuzzyConfiguration(
                new List<NumericVariable> { new NumericVariable("Var1", 42, 1701), new NumericVariable("Var2", 1, 3) },
                new List<FuzzyVariable>(),
                new List<FuzzyVariable>(),
                new List<Iteration>(),
                new List<FuzzyVariable>());

            var sut = new ConfigurationIO();
            sut.WriteToFile(config, "Test.json");

            var result = sut.ReadFromFile("Test.json");

            Assert.AreEqual(config.NumericVariables.Count, result.NumericVariables.Count);
            Assert.AreEqual(config.FuzzyVariables.Count, result.FuzzyVariables.Count);
            Assert.AreEqual(config.Iterations.Count, result.Iterations.Count);
        }
    }
}
