using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Conways2
{
    public class EnumerateCellsAndAddMissingDeadNeighbours
    {
        readonly HashSet<Point> _evaluatedPoints = new HashSet<Point>();

        public IEnumerable<Cell> Enumerate(CellList list)
        {
            List<Cell> unEvaluatedCells = list.Cells.ToList();

            foreach (Cell cell in unEvaluatedCells)
            {
                if (!NotYetEvaluated(cell)) continue;

                yield return cell;

                WasEvaluated(cell);
            }

            foreach (Cell cell in unEvaluatedCells)
            {
                List<Cell> unEvaluatedNeighbourCells = FindOrGenerateNeighbourCells(list, cell).ToList();

                foreach (Cell neighbourCell in unEvaluatedNeighbourCells)
                {
                    if (!NotYetEvaluated(neighbourCell)) continue;

                    yield return neighbourCell;

                    WasEvaluated(neighbourCell);
                }
            }

            Debug.Assert(_evaluatedPoints.Count == list.Cells.Count);
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

            foreach (Point point in points)
            {
                Cell match = new FindCellAtPosition().Find(list, point);
                if (match != null)
                {
                    yield return match;
                }
                else
                {
                    yield return new AddNewCellToList(point, CellState.Dead).ToList(list);
                }
            }
        }
    }
}