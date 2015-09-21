using System;

namespace fuzzyController.variables
{
    /// <summary>
    /// The value of a numeric variable.
    /// </summary>
    public class NumericValue
    {
        /// <summary>
        /// Creates a new numeric value.
        /// </summary>
        /// <param name="variable">The variable the value gets assigned to.</param>
        /// <param name="value">The value for the variable.</param>
        public NumericValue(NumericVariable variable, double value)
        {
            if (variable == null)
                throw new ArgumentNullException("variable");
            if (variable.MinValue > value || variable.MaxValue < value)
                throw new ArgumentOutOfRangeException("value");

            Variable = variable;
            Value = value;
        }

        /// <summary>
        /// The variable the value gets assigned to.
        /// </summary>
        public readonly NumericVariable Variable;

        /// <summary>
        /// The value for the variable.
        /// </summary>
        public readonly double Value;

        public override string ToString()
        {
            return Variable + " = " + Value;
        }
    }
}
