namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    public class AverageMaximumStrategy : MaximumDefuzzifyStrategy
    {
        protected override double getValue(double left, double right)
        {
            return (right + left) / 2;
        }

        public override string ToString()
        {
            return "Average Maximum";
        }
    }
}
