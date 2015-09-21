using System;

namespace fuzzyController.math
{
    public class LineSegment
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public double? Gradient { get; private set; }
        public double? B { get; private set; }

        public LineSegment(Point start, Point end)
        {
            if(start.X > end.X)
                throw new ArgumentException();
            Start = start;
            End = end;
            if (Math.Abs(Start.X - End.X) > 0.00000000001)
                Gradient = (End.Y - Start.Y)/(End.X - Start.X);

            if(Gradient.HasValue)
                B = Start.Y - Start.X * Gradient.Value;
        }

        public bool Contains(Point point)
        {
            if (point.Equals(Start) || point.Equals(End))
                return true;
            // point lies on line according 
            // to intersection theorem ( Strahlensatz)
            if (Math.Abs((End.Y - Start.Y)/(End.X - Start.X) - (point.Y - Start.Y)/(point.X - Start.X)) < 0.00000000001)
            {
                var minX = Math.Min(Start.X, End.X);
                var maxX = Math.Max(Start.X, End.X);
                var minY = Math.Min(Start.Y, End.Y);
                var maxY = Math.Max(Start.Y, End.Y);
                // the point lies in the rectangle, that is spanned by Start and End
                return minX <= point.X && point.X <= maxX && minY <= point.Y && point.Y <= maxY;
            }
            return false;
        }

        private static Point intersectWithVertical(double x, LineSegment segment)
        {
            if (x < Math.Min(segment.Start.X, segment.End.X) || x > Math.Max(segment.Start.X, segment.End.X))
                return null;

            var b = segment.Start.Y - segment.Start.X*segment.Gradient.Value;

            var y = segment.Gradient.Value*x + b;

            return new Point(x, y);
        }

        private static Point intersectWithHorizontal(double y, LineSegment segment)
        {
            if (y < Math.Min(segment.Start.Y, segment.End.Y) || y > Math.Max(segment.Start.Y, segment.End.Y))
                return null;

            var b = segment.Start.Y - segment.Start.X*segment.Gradient.Value;

            var x = (y - b) / segment.Gradient.Value;

            return new Point(x, y);
        }

        public Point Intersect(LineSegment segment)
        {
            if (Start.Equals(segment.End))
                return Start;
            if (End.Equals(segment.Start))
                return End;
            if ( Gradient == segment.Gradient)
                return null;
            
            if (Gradient == null)
                return intersectWithVertical(Start.X, segment);
            if (segment.Gradient == null)
                return intersectWithVertical(segment.Start.X, this);
            if (Gradient == 0)
                return intersectWithHorizontal(Start.Y, segment);
            if (segment.Gradient == 0)
                return intersectWithHorizontal(segment.Start.Y, this);

            var b1 = Start.Y - Start.X * Gradient.Value;
            var b2 = segment.Start.Y - segment.Start.X * segment.Gradient.Value;

            var x = (b1 - b2)/(segment.Gradient.Value - Gradient.Value);
            var y = Gradient.Value*x + b1;

            var point = new Point(x, y);

            if (Contains(point) && segment.Contains(point))
                return point;

            return null;
        }

        public override string ToString()
        {
            return Start + " -> " + End;
        }
    }
}
