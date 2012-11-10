namespace Conways1
{
    public class WorldToConwayModelWrapper : IConwayModel
    {
        int ticks;
        World world;

        public WorldToConwayModelWrapper(World initial)
        {
            world = initial;
        }

        public bool IsLive(int x, int y)
        {
            return world.IsCellAlive(new Position(x, y));
        }

        public void Tick()
        {
            world = world.Tick();
            ticks++;
        }

        public int TotalCells
        {
            get { return world.TotalCells; }
        }

        public int Ticks
        {
            get { return ticks; }
        }
    }
}