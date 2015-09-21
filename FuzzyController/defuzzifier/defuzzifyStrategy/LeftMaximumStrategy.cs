namespace fuzzyController.defuzzifier.defuzzifyStrategy
{
    public class LeftMaximumStrategy : MaximumDefuzzifyStrategy
    {
        protected override double getValue(double left, double right)
        {
            return left;
        }

        public override string ToString()
        {
            return "Left Maximum";
        }
    }
}
