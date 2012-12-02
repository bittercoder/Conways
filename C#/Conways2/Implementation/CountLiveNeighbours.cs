using System.Collections.Generic;
using System.Linq;

namespace Conways2.Tests
{
    public class CountLiveNeighbours
    {
        public int Count(CellList list, Point point)
        {
            var finder = new FindNeighboursOfCellAtPosition();
            IEnumerable<Cell> cells = finder.Find(list, point);
            return cells.Count(c => c.State == CellState.Live);
        }
    }
}