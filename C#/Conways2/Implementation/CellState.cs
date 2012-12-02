namespace Conways2.Tests
{
    public abstract class CellState
    {
        public static readonly CellState Dead = new DeadCellState();
        public static readonly CellState Live = new LiveCellState();
        public abstract bool IsAlive { get; }
        public abstract bool IsDead { get; }
    }
}