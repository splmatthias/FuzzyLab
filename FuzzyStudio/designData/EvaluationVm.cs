using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class EvaluationVm : EvaluationViewModel
    {
        public EvaluationVm()
        {
            UseDirectInput = false;
            NumericValues.Add(new NumericValueViewModel { Identifier = "Y__Pos", Value = 42, MinValue = 0, MaxValue = 50});
            NumericValues.Add(new NumericValueViewModel { Identifier = "Distance__Ball", Value = 4711 });
            NumericValues.Add(new NumericValueViewModel { Identifier = "Distance__Goal", Value = 1701 });

            var scope1 = new ScopeViewModel { Title = "Iteration 0" };
            var speed = new FuzzyValueViewModel { FuzzyVariable = "Speed" };
            speed.Values.Add(new TermValueViewModel { Term = "Very Slow", Value = 0.2 });
            speed.Values.Add(new TermValueViewModel { Term = "Slow", Value = 0.4 });
            speed.Values.Add(new TermValueViewModel { Term = "Medium", Value = 0.2 });
            scope1.Values.Add(speed);
            var traffic = new FuzzyValueViewModel { FuzzyVariable = "Traffic" };
            traffic.Values.Add(new TermValueViewModel { Term = "Dense", Value = 0.5 });
            traffic.Values.Add(new TermValueViewModel { Term = "Very Dense", Value = 0.8 });
            scope1.Values.Add(traffic);
            Scopes.Add(scope1);

            var scope2 = new ScopeViewModel { Title = "Iteration 1" };
            var acceleration = new FuzzyValueViewModel { FuzzyVariable = "Acceleration" };
            acceleration.Values.Add(new TermValueViewModel { Term = "Slow down", Value = 0.2 });
            acceleration.Values.Add(new TermValueViewModel { Term = "DoNothing", Value = 0.4 });
            scope2.Values.Add(acceleration);

            Scopes.Add(scope2);
        }
    }
}
