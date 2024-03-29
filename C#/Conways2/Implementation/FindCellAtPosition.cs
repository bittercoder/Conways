﻿using System.Linq;

namespace Conways2
{
    public class FindCellAtPosition
    {
        public Cell Find(CellList list, Point position)
        {
            return list.Cells.FirstOrDefault(c => c.Point == position);
        }
    }
}