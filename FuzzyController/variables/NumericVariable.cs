using System;

namespace fuzzyController.variables
{
    /// <summary>
    /// Represents a real-valued numeric variable.
    /// </summary>
    public class NumericVariable
    {
        /// <summary>
        /// Variable-Constructor
        /// </summary>
        /// <param name="identifier">The variable's identifier.</param>
        /// <param name="minValue">A optional minimum value for the variable. If left out the minimum will be double.MinValue.</param>
        /// <param name="maxValue">A optional maximum value for the variable. If left out the maximum will be double.MaxValueStrategy.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when ...
        ///     - identifier is not set.
        ///     - maxValue is less than minValue.
        /// </exception>
        public NumericVariable(string identifier, double? minValue = null, double? maxValue = null)
        {
            if (string.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier");
            if (maxValue < minValue)
                throw new ArgumentException("maxValue");

            Identifier = identifier;
            MinValue = minValue ?? double.MinValue;
            MaxValue = maxValue ?? double.MaxValue;
        }

        /// <summary>
        /// The Identifier of the variable.
        /// </summary>
        public readonly string Identifier;

        /// <summary>
        /// The miminum value this variable can Accept.
        /// </summary>
        public readonly double MinValue;

        /// <summary>
        /// The maximum value this variable can Accept.
        /// </summary>
        public readonly double MaxValue;

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as NumericVariable);
        }

        public bool Equals(NumericVariable obj)
        {
            return obj != null && obj.Identifier == Identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
}
