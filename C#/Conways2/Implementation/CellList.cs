using System.Collections.Generic;

namespace Conways2.Tests
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