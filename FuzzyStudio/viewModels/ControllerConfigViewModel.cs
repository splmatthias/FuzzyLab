using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger.strategies;
using fuzzyStudio.services;
using fuzzyStudio.views;
using System.ComponentModel.Composition;

namespace fuzzyStudio.viewModels
{
    public class ControllerConfigViewModel : ViewModel<IView>
    {
        [ImportingConstructor]
        public ControllerConfigViewModel(IConfigurationService configService,IView view) : base(view)
        {
            var defuzStrat = configService.GetDefuzzifyStrategies();
            DefuzzifyStrategies = new ListViewModel<IDefuzzifyStrategy, IView>(defuzStrat.Item1, defuzStrat.Item2);

            var evalStrat = configService.GetEvaluationStrategies();
            EvaluationStrategies = new ListViewModel<IEvaluationStrategy, IView>(evalStrat.Item1, evalStrat.Item2);

            var mergeStart = configService.GetMsfMergingStrategies();
            MergeStrategies = new ListViewModel<IMsfMergingStrategy, IView>(mergeStart.Item1, mergeStart.Item2);

            var mergingStrat = configService.GetMergingStrategies();
            MergingStrategies = new ListViewModel<IMergingStrategy, IView>(mergingStrat.Item1, mergingStrat.Item2);

            var scaleStrat = configService.GetMsfScaleStrategies();
            ScaleStrategies = new ListViewModel<IMsfScalingStrategy, IView>(scaleStrat.Item1, scaleStrat.Item2);
        }
        
        public ListViewModel<IDefuzzifyStrategy, IView> DefuzzifyStrategies { get; private set; }

        public ListViewModel<IEvaluationStrategy, IView> EvaluationStrategies { get; private set; }

        public ListViewModel<IMsfMergingStrategy, IView> MergeStrategies { get; private set; }

        public ListViewModel<IMergingStrategy, IView> MergingStrategies { get; private set; }

        public ListViewModel<IMsfScalingStrategy, IView> ScaleStrategies { get; private set; }
    }
}
