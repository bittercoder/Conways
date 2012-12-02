namespace Conways2.Tests
{
    public class DeadCellState : CellState
    {
        public override bool IsAlive
        {
            get { return false; }
        }

        public override bool IsDead
        {
            get { return true; }
        }
    }
}