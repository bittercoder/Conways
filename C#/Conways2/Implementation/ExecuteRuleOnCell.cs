namespace Conways2.Tests
{
    public abstract class ExecuteRuleOnCell
    {
        public abstract bool TryExecute(Cell cell, out AddNewCellToList resultingCell);

        protected int CountNeighbours(Cell cell)
        {
            var counter = new CountLiveNeighbours();
            int neighbours = counter.Count(cell.List, cell.Point);
            return neighbours;
        }
    }
}