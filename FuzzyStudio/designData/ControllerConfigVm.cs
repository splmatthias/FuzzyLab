using System;
using System.Collections.Generic;
using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger.strategies;
using fuzzyStudio.services;
using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class ControllerConfigVm : ControllerConfigViewModel
    {
        private class MockConfigService : IConfigurationService
        {
            Tuple<IList<IEvaluationStrategy>, IEvaluationStrategy> IConfigurationService.GetEvaluationStrategies()
            {
                var strats = new List<IEvaluationStrategy> { new MinMaxEvaluation() };
                return new Tuple<IList<IEvaluationStrategy>, IEvaluationStrategy>(strats, strats[0]);
            }

            Tuple<IList<IMergingStrategy>, IMergingStrategy> IConfigurationService.GetMergingStrategies()
            {
                var strats = new List<IMergingStrategy> { new MaxValueStrategy() };
                return new Tuple<IList<IMergingStrategy>, IMergingStrategy>(strats, strats[0]);
            }

            Tuple<IList<IMsfScalingStrategy>, IMsfScalingStrategy> IConfigurationService.GetMsfScaleStrategies()
            {
                var strats = new List<IMsfScalingStrategy> { new MinMsfScalingStrategy() };
                return new Tuple<IList<IMsfScalingStrategy>, IMsfScalingStrategy>(strats, strats[0]);
            }

            Tuple<IList<IMsfMergingStrategy>, IMsfMergingStrategy> IConfigurationService.GetMsfMergingStrategies()
            {
                var strats = new List<IMsfMergingStrategy> { new MaxMsfMergingStrategy() };
                return new Tuple<IList<IMsfMergingStrategy>, IMsfMergingStrategy>(strats, strats[0]);
            }

            Tuple<IList<IDefuzzifyStrategy>, IDefuzzifyStrategy> IConfigurationService.GetDefuzzifyStrategies()
            {
                var strats = new List<IDefuzzifyStrategy> { new LeftMaximumStrategy() };
                return new Tuple<IList<IDefuzzifyStrategy>, IDefuzzifyStrategy>(strats, strats[0]);
            }
        }

        public ControllerConfigVm()
            : base(new MockConfigService(), null)
        {
            EvaluationStrategies.SelectedItem = EvaluationStrategies.Items[0];
            
        }
    }
}
