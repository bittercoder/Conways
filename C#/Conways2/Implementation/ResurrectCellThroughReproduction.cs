namespace Conways2
{
    public class ResurrectCellThroughReproduction : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out AddNewCellToList resultingCell)
        {
            if (CountNeighbours(cell) == 3)
            {
                resultingCell = new AddNewCellToList(cell.Point, CellState.Live);
                return true;
            }
            resultingCell = null;
            return false;
        }
    }
}