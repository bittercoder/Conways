namespace Conways2.Tests
{
    public class AddNewCellToList
    {
        readonly Point _position;
        readonly CellState _state;

        public AddNewCellToList(Point position, CellState state)
        {
            _position = position;
            _state = state;
        }

        public Cell ToList(CellList list)
        {
            var cell = new Cell(list, _position, _state);
            list.Cells.Add(cell);
            return cell;
        }
    }
}