using Xunit;

namespace Conways2
{
    public class Tests
    {
        readonly CellList list = new CellList();
        AddNewCellToList result;
        CellList resultList = new CellList();

        [Fact]
        public void any_live_cell_with_fewer_than_two_live_neighbours_dies_when_applying_kill_underpopulated_cell_rule()
        {
            AddLiveCell(0, 0);
            var killer = new KillUnderpoulatedCell();
            killer.TryExecute(GetCellAt(0, 0), out result);
            result.ToList(resultList);
            Assert.Equal(CellState.Dead, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void any_live_cell_with_more_than_three_neighbours_dies_when_applying_kill_overpopulated_cell_rule()
        {
            AddLiveCell(0, 0);
            AddLiveCell(1, 0);
            AddLiveCell(0, 1);
            AddLiveCell(-1, 0);
            AddLiveCell(0, -1);
            var killer = new KillOverpopulatedCell();
            killer.TryExecute(GetCellAt(0, 0), out result);
            result.ToList(resultList);
            Assert.Equal(CellState.Dead, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void any_dead_cell_with_exactly_three_live_neighbours_becomes_alive_when_applying_resurrect_cell_through_reproduction_rule()
        {
            AddDeadCell(0, 0);
            AddLiveCell(1, 0);
            AddLiveCell(0, 1);
            AddLiveCell(-1, 0);
            var resurrector = new ResurrectCellThroughReproduction();
            resurrector.TryExecute(GetCellAt(0, 0), out result);
            result.ToList(resultList);
            Assert.Equal(CellState.Live, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void get_next_generation_of_cells_for_cell_with_fewer_than_two_live_neighbours_kills_cell()
        {
            AddLiveCell(0, 0);
            ExecuteGameRules();
            Assert.Equal(CellState.Dead, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void get_next_generation_of_cells_for_cell_with_two_live_neighbours_keeps_cell_alive()
        {
            AddLiveCell(0, 0);
            AddLiveCell(0, 1);
            AddLiveCell(1, 0);
            ExecuteGameRules();
            Assert.Equal(CellState.Live, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void get_next_generation_of_cells_for_cell_with_four_live_neighbours_kills_cell()
        {
            AddLiveCell(0, 0);
            AddLiveCell(1, 0);
            AddLiveCell(0, 1);
            AddLiveCell(-1, 0);
            AddLiveCell(0, -1);
            ExecuteGameRules();
            Assert.Equal(CellState.Dead, GetResultCellAt(0, 0).State);
        }
        
        [Fact]
        public void get_next_generation_of_cells_evaluated_neighbours()
        {
            //  ___     -1
            //  _XX     0 
            //  _X_     1
            //
            //  -1 0 1

            AddLiveCell(0, 0);
            AddLiveCell(1, 0);
            AddLiveCell(0, 1);
            ExecuteGameRules();
            Assert.Equal(CellState.Live, GetResultCellAt(0, 0).State);
            Assert.Equal(CellState.Live, GetResultCellAt(1, 0).State);
            Assert.Equal(CellState.Live, GetResultCellAt(0, 1).State);
        }

        [Fact]
        public void get_next_generation_of_cells_when_empty_cell_surrounded_by_3_neighbours_brings_empty_cell_to_life()
        {
            AddLiveCell(-1, 0);
            AddLiveCell(0, -1);
            AddLiveCell(1, 1);
            ExecuteGameRules();
            Assert.Equal(CellState.Live, GetResultCellAt(0, 0).State);
        }

        void ExecuteGameRules()
        {
            var executor = new ExecuteConwayRules();
            resultList = executor.GetNextGeneration(list);
        }

        void AddLiveCell(int x, int y)
        {
            new AddNewCellToList(new Point(x, y), CellState.Live).ToList(list);
        }

        void AddDeadCell(int x, int y)
        {
            new AddNewCellToList(new Point(x, y), CellState.Dead).ToList(list);
        }

        Cell GetCellAt(int x, int y)
        {
            return new FindCellAtPosition().Find(list, new Point(x, y));
        }

        Cell GetResultCellAt(int x, int y)
        {
            return new FindCellAtPosition().Find(resultList, new Point(x, y));
        }
    }
}