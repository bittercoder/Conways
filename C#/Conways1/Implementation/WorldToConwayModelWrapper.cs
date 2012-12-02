using ConwayConsole;

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

        public bool Tick()
        {
            world = world.Tick();
            ticks++;
            return true;
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