using NUnit.Framework;

namespace fuzzyController.test
{
    [TestFixture]
    public class FuzzyControllerTest
    {
    //    [Test]
    //    public void Constructor_Fails_UnresolvedNumericVariable()
    //    {
    //        var mocks = new MockRepository();

    //        var controllerConfiguration = new ControllerConfiguration(
    //            mocks.Stub<IFuzzifier>(),
    //            mocks.Stub<IRuleEvaluation>(), 
    //            mocks.Stub<IFuzzyValueMerger>(), 
    //            mocks.Stub<IDefuzzifier>()
    //        );

    //        var numericVariable = new NumericVariable("NumVar");
    //        var fuzzyConfiguration = new FuzzyConfiguration(
    //            new NumericVariable[] {},
    //            new[] { new FuzzyVariable("Var", numericVariable) }, 
    //            new FuzzyImplication[] {}
    //        );

    //        Assert.AreEqual(numericVariable, Assert.Throws<UnresolvedNumericVariableException>(() => new FuzzyController(fuzzyConfiguration, controllerConfiguration)).Variable);
    //    }

    //    [Test]
    //    public void Constructor_Fails_UnresolvedFuzzyVariable()
    //    {
    //        var mocks = new MockRepository();

    //        var controllerConfiguration = new ControllerConfiguration(
    //            mocks.Stub<IFuzzifier>(),
    //            mocks.Stub<IRuleEvaluation>(),
    //            mocks.Stub<IFuzzyValueMerger>(),
    //            mocks.Stub<IDefuzzifier>()
    //        );

    //        var numericVariable = new NumericVariable("NumVar");
    //        var fuzzyTerms = new FuzzyTerm("Term", new MembershipFunction());
    //        var fuzzyVariable = new FuzzyVariable("Var", numericVariable, fuzzyTerms);
    //        var fuzzyConfiguration = new FuzzyConfiguration(
    //            new[] {numericVariable},
    //            new FuzzyVariable[] { },
    //            new[] { new FuzzyImplication(new ValueExpression(fuzzyVariable, fuzzyTerms), new ValueExpression(fuzzyVariable, fuzzyTerms)) }
    //        );

    //        Assert.AreEqual(fuzzyVariable, Assert.Throws<UnresolvedFuzzyVariableException>(() => new FuzzyController(fuzzyConfiguration, controllerConfiguration)).Variable);
    //    }

    //    [Test]
    //    public void Constructor_Fails_CyclicDependencies()
    //    {
    //        var mocks = new MockRepository();

    //        var controllerConfiguration = new ControllerConfiguration(
    //            mocks.Stub<IFuzzifier>(),
    //            mocks.Stub<IRuleEvaluation>(),
    //            mocks.Stub<IFuzzyValueMerger>(),
    //            mocks.Stub<IDefuzzifier>()
    //        );

    //        var numericVariable1 = new NumericVariable("NumVar1");
    //        var numericVariable2 = new NumericVariable("NumVar2");
    //        var fuzzyTerms1 = new FuzzyTerm("Term1", new MembershipFunction());
    //        var fuzzyTerms2 = new FuzzyTerm("Term2", new MembershipFunction());
    //        var fuzzyVariable1 = new FuzzyVariable("Var1", numericVariable1, fuzzyTerms1);
    //        var fuzzyVariable2 = new FuzzyVariable("Var2", numericVariable2, fuzzyTerms2);
    //        var fuzzyImplication1 = new FuzzyImplication(new ValueExpression(fuzzyVariable1, fuzzyTerms1), new ValueExpression(fuzzyVariable2, fuzzyTerms2));
    //        var fuzzyImplication2 = new FuzzyImplication(new ValueExpression(fuzzyVariable2, fuzzyTerms2), new ValueExpression(fuzzyVariable1, fuzzyTerms1));
    //        var fuzzyConfiguration = new FuzzyConfiguration(
    //                new[] {numericVariable1, numericVariable2},
    //                new[] {fuzzyVariable1, fuzzyVariable2},
    //                new[] {fuzzyImplication1, fuzzyImplication2}
    //            );
    //        var affectedRules = Assert.Throws<CyclicDependenciesException>(() => new FuzzyController(fuzzyConfiguration, controllerConfiguration)).AffectedImplications;
    //        Assert.AreEqual(2, affectedRules.Count);
    //        Assert.IsTrue(affectedRules.Contains(fuzzyImplication1));
    //        Assert.IsTrue(affectedRules.Contains(fuzzyImplication2));
    //    }
    }
}
