using fuzzyController;
using fuzzyController.defuzzifier.defuzzifyStrategy;
using fuzzyController.defuzzifier.msfMergingStrategy;
using fuzzyController.defuzzifier.msfScalingStrategy;
using fuzzyController.inference.evaluation;
using fuzzyController.inference.valueMerger.strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace fuzzyStudio.services
{
    [Export]
    public class ConfigurationService : IConfigurationService
    {
        public Tuple<IList<IEvaluationStrategy>, IEvaluationStrategy>  GetEvaluationStrategies()
        {
            return getAllImplementations<IEvaluationStrategy>();
        }

        public Tuple<IList<IMergingStrategy>, IMergingStrategy> GetMergingStrategies()
        {
            return getAllImplementations<IMergingStrategy>();
        }
        
        public Tuple<IList<IMsfScalingStrategy>, IMsfScalingStrategy> GetMsfScaleStrategies()
        {
            return getAllImplementations<IMsfScalingStrategy>();
        }

        public Tuple<IList<IMsfMergingStrategy>, IMsfMergingStrategy> GetMsfMergingStrategies()
        {
            return getAllImplementations<IMsfMergingStrategy>();
        }

        public Tuple<IList<IDefuzzifyStrategy>, IDefuzzifyStrategy> GetDefuzzifyStrategies()
        {
            return getAllImplementations<IDefuzzifyStrategy>();
        }

        private Tuple<IList<T>, T> getAllImplementations<T>() where T : class
        {
            var type = typeof(T);
            var types = type.Assembly.GetTypes();

            var impls = types
                .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract && t.IsClass)
                .Select(t => Activator.CreateInstance(t) as T).ToList();
            var defaultInstance = getDefault(impls);

            return new Tuple<IList<T>, T>(impls, defaultInstance);
        }

        private T getDefault<T>(IEnumerable<T> items) where T : class
        {
            return items
                .First(t => Attribute.GetCustomAttribute(t.GetType(), typeof(DefaultAttribute)) != null);
        }
    }
}
