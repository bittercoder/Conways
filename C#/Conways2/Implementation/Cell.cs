namespace Conways2.Tests
{
    public class Cell
    {
        public Cell(CellList list, Point point, CellState state)
        {
            Point = point;
            State = state;
            List = list;
        }

        public CellList List { get; private set; }
        public Point Point { get; private set; }
        public CellState State { get; private set; }
    }
}