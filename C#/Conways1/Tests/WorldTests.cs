using Xunit;

namespace Conways1
{
    public class WorldTests
    {
        [Fact]
        public void get_neighbours_for_cell_with_no_neighbours_returns_0()
        {
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            Assert.Equal(0, world.GetLiveNeighbours(new Position(0, 0)));
        }

        [Fact]
        public void get_neighbours_for_cell_with_one_neighbour_returns_1()
        {
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            world.AddLiveCell(new Position(1, 1));
            Assert.Equal(1, world.GetLiveNeighbours(new Position(0, 0)));
        }

        [Fact]
        public void tick_world_returns_new_world_with_cells_after_applying_rules()
        {
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            World worldAftertick = world.Tick();
            Assert.True(worldAftertick.IsCellDead(new Position(0, 0)));
        }

        [Fact]
        public void tick_world_brings_dead_cells_to_life_when_has_3_neighbours()
        {
            var world = new World();

            world.SetupWorld(
                "0+0", // <- we expect the cell at 2,0 to come alive because it has 3 neighbours
                "0++",
                "000");

            World worldAfterTick = world.Tick();

            Assert.True(worldAfterTick.IsCellAlive(new Position(2, 0)));
        }
    }
}