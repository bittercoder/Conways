using System.Linq;

namespace Conways1
{
    public static class WorldExtensions
    {
        public static World SetupWorld(this World world, params string[] rows)
        {
            foreach (var row in rows.Select((cells, y) => new {cells = cells.ToCharArray(), y}))
            {
                foreach (var cell in row.cells.Select((value, x) => new {value, x}))
                {
                    if (cell.value != ' ')
                    {
                        world.AddLiveCell(new Position(cell.x, row.y));
                    }
                }
            }

            return world;
        }
    }
}