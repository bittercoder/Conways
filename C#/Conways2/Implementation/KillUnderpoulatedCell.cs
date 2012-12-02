namespace Conways2.Tests
{
    public class KillUnderpoulatedCell : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out AddNewCellToList resultingCell)
        {
            if (CountNeighbours(cell) < 2)
            {
                resultingCell = new AddNewCellToList(cell.Point, CellState.Dead);
                return true;
            }
            resultingCell = null;
            return false;
        }
    }
}