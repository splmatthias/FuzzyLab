using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger.strategies;
using System;
using System.Collections.Generic;

namespace fuzzyStudio.services
{
    public interface IConfigurationService
    {
        Tuple<IList<IEvaluationStrategy>, IEvaluationStrategy> GetEvaluationStrategies();

        Tuple<IList<IMergingStrategy>, IMergingStrategy> GetMergingStrategies();

        Tuple<IList<IMsfScalingStrategy>, IMsfScalingStrategy> GetMsfScaleStrategies();

        Tuple<IList<IMsfMergingStrategy>, IMsfMergingStrategy> GetMsfMergingStrategies();

        Tuple<IList<IDefuzzifyStrategy>, IDefuzzifyStrategy> GetDefuzzifyStrategies();
    }
}
