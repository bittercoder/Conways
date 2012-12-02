using System.Collections.Generic;
using System.Linq;

namespace Conways2.Tests
{
    public class EnumerateCellsAndAddMissingDeadNeighbours
    {
        readonly HashSet<Point> _evaluatedPoints = new HashSet<Point>();

        public IEnumerable<Cell> Enumerate(CellList list)
        {
            var unEvaluatedCells = list.Cells.ToList().Where(NotYetEvaluated);

            foreach (Cell cell in unEvaluatedCells)
            {
                yield return cell;

                WasEvaluated(cell);

                var unEvaluatedNeighbourCells = FindOrGenerateNeighbourCells(list, cell).Where(NotYetEvaluated);

                foreach (Cell neighbourCell in unEvaluatedNeighbourCells)
                {
                    yield return neighbourCell;

                    WasEvaluated(neighbourCell);
                }
            }
        }

        void WasEvaluated(Cell cell)
        {
            _evaluatedPoints.Add(cell.Point);
        }

        bool NotYetEvaluated(Cell cell)
        {
            return !_evaluatedPoints.Contains(cell.Point);
        }

        IEnumerable<Cell> FindOrGenerateNeighbourCells(CellList list, Cell cell)
        {
            IEnumerable<Point> points = new GeneratePointsAroundPoint().Generate(cell.Point);

            foreach (var point in points)
            {
                var match = new FindCellAtPosition().Find(list, point);
                if (match != null) yield return match;
                yield return new AddNewCellToList(point, CellState.Dead).ToList(list);
            }
        }
    }
}