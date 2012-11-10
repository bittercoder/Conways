namespace Conways1
{
    public class Cell
    {
        protected Cell(Position position)
        {
            Position = position;
        }

        public bool IsDead { get; private set; }

        public bool IsAlive
        {
            get { return !IsDead; }
        }

        public Position Position { get; private set; }

        public static Cell DeadAt(Position position)
        {
            return new Cell(position) {IsDead = true};
        }

        public static Cell LiveAt(Position position)
        {
            return new Cell(position);
        }

        public Cell Tick(int liveNeighbours)
        {
            if (IsDead && liveNeighbours == 3) return Live();
            if (liveNeighbours < 2) return Die();
            if (liveNeighbours > 3) return Die();
            return this;
        }

        Cell Die()
        {
            return new Cell(Position) {IsDead = true};
        }

        Cell Live()
        {
            return new Cell(Position);
        }

        public bool IsAtPosition(Position position)
        {
            return Position.Equals(position);
        }
    }
}