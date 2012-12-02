using System;

namespace Conways2.Tests
{
    public class Point : IEquatable<Point>
    {
        readonly int _x;
        readonly int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x*397) ^ _y;
            }
        }

        public static bool operator ==(Point left, Point right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !Equals(left, right);
        }

        public static Point OffsetFrom(Point point, int xOffset, int yOffset)
        {
            return new Point(point._x + xOffset, point._y + yOffset);
        }
    }
}