using System;
using System.Linq;

namespace ConwayConsole
{
    public static class CellsSetupUtility
    {
        public static void SetupCells(Action<int, int> createLiveCell, params string[] rows)
        {
            foreach (var row in rows.Select((cells, y) => new { cells = cells.ToCharArray(), y }))
            {
                foreach (var cell in row.cells.Select((value, x) => new { value, x }))
                {
                    if (cell.value != ' ')
                    {
                        createLiveCell(cell.x, row.y);
                    }
                }
            }
        }
    }
}