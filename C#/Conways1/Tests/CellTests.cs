using Xunit;

namespace Conways1
{
    public class CellTests
    {
        readonly Cell deadCell = Cell.DeadAt(new Position(0, 0));
        readonly Cell liveCell = Cell.LiveAt(new Position(0, 0));

        [Fact]
        public void New_cells_are_alive()
        {
            Assert.False(liveCell.IsDead);
        }

        [Fact]
        public void Any_live_cell_with_one_live_neighbours_dies()
        {
            Cell cellAfterTick = liveCell.Tick(1);
            Assert.True(cellAfterTick.IsDead);
        }

        [Fact]
        public void Any_live_cell_with_no_live_neighbours_dies()
        {
            Cell cellAfterTick = liveCell.Tick(0);
            Assert.True(cellAfterTick.IsDead);
        }

        [Fact]
        public void Any_live_cell_with_two_live_neighbours_lives()
        {
            Cell cellAfterTick = liveCell.Tick(2);
            Assert.False(cellAfterTick.IsDead);
        }

        [Fact]
        public void Any_live_cell_with_three_live_neighbours_lives()
        {
            Cell cellAfterTick = liveCell.Tick(3);
            Assert.False(cellAfterTick.IsDead);
        }

        [Fact]
        public void Any_live_cell_with_more_then_3_live_neighbours_dies()
        {
            Cell cellAfterTick = liveCell.Tick(4);
            Assert.True(cellAfterTick.IsDead);
        }

        [Fact]
        public void Any_dead_cell_with_exactly_three_live_neighbours_becomes_a_live_cell()
        {
            Cell cellAfterTick = deadCell.Tick(3);
            Assert.False(cellAfterTick.IsDead);
        }
    }
}