using System.Collections.Generic;

namespace Conways2
{
    public class GeneratePointsAroundPoint
    {
        public IEnumerable<Point> Generate(Point point)
        {
            var offsets = new[] {-1, 0, 1};

            foreach (int xOffset in offsets)
            {
                foreach (int yOffset in offsets)
                {
                    if (xOffset == 0 && yOffset == 0) continue;
                    yield return Point.OffsetFrom(point, xOffset, yOffset);
                }
            }
        }
    }
}