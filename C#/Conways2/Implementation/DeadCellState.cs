namespace Conways2
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

        public override string ToString()
        {
            return "Dead";
        }
    }
}