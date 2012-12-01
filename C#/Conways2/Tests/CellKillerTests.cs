using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Conways2.Tests
{
    public class Tests
    {
        Cell resultCell;

        [Fact]
        public void any_live_cell_with_fewer_than_two_live_neighbours_dies_when_applying_kill_underpopulated_cell_rule()
        {
            var cell = new Cell(0, 0);
            var killer = new KillUnderpoulatedCell();
            killer.TryExecute(cell, out resultCell);
            Assert.False(resultCell.IsAlive);
        }

        [Fact]
        public void any_live_cell_with_more_than_three_neighbours_dies_when_applying_kill_overpopulated_cell_rule()
        {
            var cell = new Cell(0, 0);
            cell.HasNeighbourAt(1, 0);
            cell.HasNeighbourAt(0, 1);
            cell.HasNeighbourAt(-1, 0);
            cell.HasNeighbourAt(0, -1);
            var killer = new KillOverpopulatedCell();
            killer.TryExecute(cell, out resultCell);
            Assert.False(resultCell.IsAlive);
        }

        [Fact]
        public void any_dead_cell_with_exactly_three_live_neighbours_becomes_alive_when_applying_resurrect_cell_through_reproduction_rule()
        {
            Cell cell = Cell.Dead(0, 0);
            cell.HasNeighbourAt(1, 0);
            cell.HasNeighbourAt(0, 1);
            cell.HasNeighbourAt(-1, 0);

            var resurrector = new ResurrectCellThroughReproduction();
            resurrector.TryExecute(cell, out resultCell);
            Assert.True(resultCell.IsAlive);
        }

        [Fact]
        public void tick_cells_for_cell_with_fewer_than_two_live_neighbours_kills_cell()
        {
            var cell = new Cell(0, 0);
            var executor = new ExecuteConwayRules();
            resultCell = executor.ExecuteOn(cell);
            Assert.False(resultCell.IsAlive);
        }

        [Fact]
        public void tick_cells_for_cell_with_two_live_neighbours_keeps_cell_alive()
        {
            var cell = new Cell(0, 0);
            cell.HasNeighbourAt(1, 0);
            cell.HasNeighbourAt(0, 1);
            var executor = new ExecuteConwayRules();
            resultCell = executor.ExecuteOn(cell);
            Assert.True(resultCell.IsAlive);
        }

        [Fact]
        public void tick_cells_for_cell_with_four_live_neighbours_kills_cell()
        {
            var cell = new Cell(0, 0);
            cell.HasNeighbourAt(1, 0);
            cell.HasNeighbourAt(0, 1);
            cell.HasNeighbourAt(-1, 0);
            cell.HasNeighbourAt(0, -1);
            var executor = new ExecuteConwayRules();
            resultCell = executor.ExecuteOn(cell);
            Assert.False(resultCell.IsAlive);
        }
    }

    public class ExecuteConwayRules
    {
        public Cell ExecuteOn(Cell cell)
        {
            Cell resultingCell;
            var underpopulationKiller = new KillUnderpoulatedCell();
            if (underpopulationKiller.TryExecute(cell, out resultingCell)) return resultingCell;

            var overpopulationKiller = new KillOverpopulatedCell();
            if (overpopulationKiller.TryExecute(cell, out resultingCell)) return resultingCell;

            var resurrector = new ResurrectCellThroughReproduction();
            if (resurrector.TryExecute(cell, out resultingCell)) return resultingCell;

            return cell.Clone();
        }
    }

    public abstract class ExecuteRuleOnCell
    {
        public abstract bool TryExecute(Cell cell, out Cell resultingCell);

        protected int CountNeighbours(Cell cell)
        {
            var counter = new CountLiveNeighbours();
            int neighbours = counter.Count(cell);
            return neighbours;
        }
    }

    public class ResurrectCellThroughReproduction : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out Cell resultingCell)
        {
            if (CountNeighbours(cell) == 3)
            {
                resultingCell = cell.Resurrect();
                return true;
            }
            resultingCell = null;
            return false;
        }
    }

    public class KillOverpopulatedCell : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out Cell resultingCell)
        {
            if (CountNeighbours(cell) > 3)
            {
                resultingCell = cell.Kill();
                return true;
            }
            resultingCell = null;
            return false;
        }
    }

    public class CountLiveNeighbours
    {
        public int Count(Cell cell)
        {
            return cell.Neighbours.Count(c => c.IsAlive);
        }
    }

    public class KillUnderpoulatedCell : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out Cell resultingCell)
        {
            if (CountNeighbours(cell) < 2)
            {
                resultingCell = cell.Kill();
                return true;
            }
            resultingCell = null;
            return false;
        }
    }

    public class Cell
    {
        readonly List<Cell> _neighbours = new List<Cell>();
        readonly int _x;
        readonly int _y;
        bool _isAlive;

        public Cell(int x, int y)
        {
            _isAlive = true;
            _x = x;
            _y = y;
        }

        public IEnumerable<Cell> Neighbours
        {
            get { return _neighbours; }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
        }

        public static Cell Dead(int x, int y)
        {
            return new Cell(x, y) {_isAlive = false};
        }

        public Cell Kill()
        {
            if (!_isAlive) throw new InvalidOperationException("You can not kill a dead cell");
            return new Cell(_x, _y) {_isAlive = false};
        }

        public void HasNeighbourAt(int xOffset, int yOffset)
        {
            _neighbours.Add(new Cell(_x + xOffset, _y + yOffset));
        }

        public Cell Resurrect()
        {
            if (_isAlive) throw new InvalidOperationException("You can not resurrect a live cell");
            return new Cell(_x, _y) {_isAlive = true};
        }

        public Cell Clone()
        {
            return new Cell(_x, _y) {_isAlive = IsAlive};
        }
    }
}