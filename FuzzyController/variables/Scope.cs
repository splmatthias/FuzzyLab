using System.Collections.Generic;

namespace fuzzyController.variables
{
    public class Scope: IEnumerable<FuzzyValue>
    {
        public Scope(params FuzzyValue[] values)
        {
            _values = new List<FuzzyValue>(values);
        }

        public Scope(IEnumerable<FuzzyValue> values)
        {
            _values = new List<FuzzyValue>(values);
        }

        public FuzzyValue this[int index] { get { return _values[index]; } }

        public int Count { get { return _values.Count; } }

        public IEnumerator<FuzzyValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        private readonly List<FuzzyValue> _values;
    }
}
