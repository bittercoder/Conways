using ConwayConsole;

namespace Conways2
{
    internal class CellListToConwayModelWrapper : IConwayModel
    {
        CellList list;
        int ticks;

        public CellListToConwayModelWrapper(CellList initial)
        {
            list = initial;
        }

        public int TotalCells
        {
            get { return list.Cells.Count; }
        }

        public int Ticks
        {
            get { return ticks; }
        }

        public bool IsLive(int x, int y)
        {
            Cell match = new FindCellAtPosition().Find(list, new Point(x, y));
            if (match == null) return false;
            return match.State == CellState.Live;
        }

        public bool Tick()
        {
            var executor = new ExecuteConwayRules();
            list = executor.GetNextGeneration(list);
            ticks++;
            return true;
        }
    }
}