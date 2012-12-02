using ConwayConsole;

namespace Conways1
{
    public static class WorldExtensions
    {
        public static World SetupWorld(this World world, params string[] rows)
        {
            CellsSetupUtility.SetupCells((x, y) => world.AddLiveCell(new Position(x, y)), rows);

            return world;
        }
    }
}