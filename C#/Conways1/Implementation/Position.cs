using System;
using System.Collections.Generic;

namespace Conways1
{
    public sealed class Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        public IEnumerable<Position> GetNeighbours()
        {
            var offests = new[] {-1, 0, 1};

            foreach (int xOffset in offests)
            {
                foreach (int yOffset in offests)
                {
                    if (xOffset == 0 && yOffset == 0) continue;

                    int xOfNeighbour = X + xOffset;
                    int yOfNeightbour = Y + yOffset;

                    yield return new Position(xOfNeighbour, yOfNeightbour);
                }
            }
        }
    }
}