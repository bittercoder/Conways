using System;
using System.Linq;

namespace Conways2
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
            if (list.Cells.Any(c => c.Point == _position))
            {
                throw new Exception("A cell already exists in that position");
            }

            var cell = new Cell(list, _position, _state);

            list.Cells.Add(cell);

            return cell;
        }
    }
}