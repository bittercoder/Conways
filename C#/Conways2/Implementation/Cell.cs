using System.Diagnostics;

namespace Conways2
{
    [DebuggerDisplay("Point: {Point}, State: {State}")]
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

        public override string ToString()
        {
            return string.Format("Point: {0}, State: {1}", Point, State);
        }
    }
}