using ConwayConsole;

namespace Conways2
{
    public static class CellListExtensions
    {
        public static CellList SetupCells(this CellList list, params string[] rows)
        {
            CellsSetupUtility.SetupCells((x, y) => new AddNewCellToList(new Point(x, y), CellState.Live).ToList(list), rows);
            return list;
        }
    }
}