namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    public class RightMaximumStrategy : MaximumDefuzzifyStrategy
    {
        protected override double getValue(double left, double right)
        {
            return right;
        }

        public override string ToString()
        {
            return "Right Maximum";
        }
    }
}
