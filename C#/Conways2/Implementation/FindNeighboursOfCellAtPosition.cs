using System.Collections.Generic;
using System.Linq;

namespace Conways2
{
    public class FindNeighboursOfCellAtPosition
    {
        public IEnumerable<Cell> Find(CellList list, Point position)
        {
            var generator = new GeneratePointsAroundPoint();

            IEnumerable<Point> points = generator.Generate(position);

            var cellFinder = new FindCellAtPosition();

            return points.Select(p => cellFinder.Find(list, p)).Where(c => c != null);
        }
    }
}