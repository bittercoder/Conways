namespace Conways2
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

        public override string ToString()
        {
            return "Live";
        }
    }
}