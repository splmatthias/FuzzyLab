using System;

namespace fuzzyController.variables
{
    /// <summary>
    /// A linguistic term of a fuzzy variable.
    /// </summary>
    public class FuzzyTerm
    {
        /// <summary>
        /// Constructs a FuzzyTerm.
        /// </summary>
        /// <param name="term">The linguistic value.</param>
        /// <param name="membershipFunction">The membership function.</param>
        public FuzzyTerm(string term, MembershipFunction membershipFunction)
        {
            if (string.IsNullOrEmpty(term))
                throw new ArgumentException("term");
            if (membershipFunction == null)
                throw new ArgumentNullException("membershipFunction");
            Term = term;
            MembershipFunction = membershipFunction;
        }

        /// <summary>
        /// The linguistic value.
        /// </summary>
        public readonly string Term;

        /// <summary>
        /// This functions determines the membership of a numeric
        /// value for this term.
        /// </summary>
        public readonly MembershipFunction MembershipFunction;

        public override string ToString()
        {
            return Term;
        }
    }
}
