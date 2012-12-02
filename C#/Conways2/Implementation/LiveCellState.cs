namespace Conways2.Tests
{
    public class LiveCellState : CellState
    {
        public override bool IsAlive
        {
            get { return true; }
        }

        public override bool IsDead
        {
            get { return false; }
        }
    }
}