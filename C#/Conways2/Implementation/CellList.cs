using System.Collections.Generic;

namespace Conways2
{
    public class CellList
    {
        public CellList()
        {
            Cells = new List<Cell>();
        }

        public List<Cell> Cells { get; set; }
    }
}