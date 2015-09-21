using System.Collections.Generic;
using System.Linq;
using fuzzyController.inference.valueMerger.strategies;
using fuzzyController.variables;

namespace fuzzyController.inference.valueMerger
{
    public class FuzzyValueMerger : IFuzzyValueMerger
    {
        private readonly IMergingStrategy _mergingStrategy;

        public FuzzyValueMerger(IMergingStrategy mergingStrategy)
        {
            _mergingStrategy = mergingStrategy;
        }

        public IList<FuzzyValue> Apply(IEnumerable<FuzzyValue> fuzzyValues)
        {
            var collectedValues = collectAllVariablesAndValues(fuzzyValues);

            var result = new List<FuzzyValue>();

            foreach (var item in collectedValues)
            {
                var values = new Dictionary<FuzzyTerm, double>();
                foreach (var value in item.Value)
                {
                    var mergedValue = _mergingStrategy.Merge(value.Value);
                    values.Add(value.Key, mergedValue);
                }
                result.Add(new FuzzyValue(item.Key, values));
            }

            return result;
        }

        private static Dictionary<FuzzyVariable, IDictionary<FuzzyTerm, IList<double>>> collectAllVariablesAndValues(IEnumerable<FuzzyValue> fuzzyValues)
        {
            var collectedValues = new Dictionary<FuzzyVariable, IDictionary<FuzzyTerm, IList<double>>>();

            foreach (var fuzzyValue in fuzzyValues)
            {
                IDictionary<FuzzyTerm, IList<double>> values;
                if (collectedValues.TryGetValue(fuzzyValue.AssociatedVariable, out values))
                {
                    foreach (var item in fuzzyValue.Values)
                    {
                        IList<double> vd;
                        if (values.TryGetValue(item.Key, out vd))
                            vd.Add(item.Value);
                        else
                            values[item.Key] = new List<double> {item.Value};
                    }
                }
                else
                {
                    var initValues =
                        fuzzyValue.Values.ToDictionary<KeyValuePair<FuzzyTerm, double>, FuzzyTerm, IList<double>>(
                            v => v.Key,
                            v => new List<double> {v.Value});
                    collectedValues.Add(fuzzyValue.AssociatedVariable, initValues);
                }
            }
            return collectedValues;
        }
    }
}
