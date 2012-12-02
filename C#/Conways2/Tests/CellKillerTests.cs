using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Conways2.Tests
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
            var executor = new ExecuteConwayRules();
            resultList = executor.GetNextGeneration(list);
            Assert.Equal(CellState.Dead, GetResultCellAt(0, 0).State);
        }

        [Fact]
        public void get_next_generation_of_cells_for_cell_with_two_live_neighbours_keeps_cell_alive()
        {
            AddLiveCell(0, 0);
            AddLiveCell(0, 1);
            AddLiveCell(1, 0);
            var executor = new ExecuteConwayRules();
            resultList = executor.GetNextGeneration(list);
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
            var executor = new ExecuteConwayRules();
            resultList = executor.GetNextGeneration(list);
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
            var executor = new ExecuteConwayRules();
            resultList = executor.GetNextGeneration(list);
            Assert.Equal(CellState.Live, GetResultCellAt(0, 0).State);
            Assert.Equal(CellState.Live, GetResultCellAt(1, 0).State);
            Assert.Equal(CellState.Live, GetResultCellAt(0, 1).State);
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

    public class ExecuteConwayRules
    {
        public CellList GetNextGeneration(CellList list)
        {
            IEnumerable<AddNewCellToList> results = list.Cells.Select(EvaluateNextGenerationOfCell);

            var nextGeneration = new CellList();

            foreach (AddNewCellToList result in results)
            {
                result.ToList(nextGeneration);
            }

            return nextGeneration;
        }

        static AddNewCellToList EvaluateNextGenerationOfCell(Cell cell)
        {
            var rules = new ExecuteRuleOnCell[] { new KillUnderpoulatedCell(), new KillOverpopulatedCell(), new ResurrectCellThroughReproduction() };

            foreach (var rule in rules)
            {
                AddNewCellToList resultingCell;
                if (rule.TryExecute(cell, out resultingCell)) return resultingCell;
            }

            return new AddNewCellToList(cell.Point, cell.State);
        }
    }

    public abstract class ExecuteRuleOnCell
    {
        public abstract bool TryExecute(Cell cell, out AddNewCellToList resultingCell);

        protected int CountNeighbours(Cell cell)
        {
            var counter = new CountLiveNeighbours();
            int neighbours = counter.Count(cell.List, cell.Point);
            return neighbours;
        }
    }

    public class ResurrectCellThroughReproduction : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out AddNewCellToList resultingCell)
        {
            if (CountNeighbours(cell) == 3)
            {
                resultingCell = new AddNewCellToList(cell.Point, CellState.Live);
                return true;
            }
            resultingCell = null;
            return false;
        }
    }

    public class KillOverpopulatedCell : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out AddNewCellToList resultingCell)
        {
            if (CountNeighbours(cell) > 3)
            {
                resultingCell = new AddNewCellToList(cell.Point, CellState.Dead);
                return true;
            }

            resultingCell = null;
            return false;
        }
    }

    public class CountLiveNeighbours
    {
        public int Count(CellList list, Point point)
        {
            var finder = new FindNeighboursOfCellAtPosition();
            IEnumerable<Cell> cells = finder.Find(list, point);
            return cells.Count(c => c.State == CellState.Live);
        }
    }

    public class AddNewCellToList
    {
        readonly Point _position;
        readonly CellState _state;

        public AddNewCellToList(Point position, CellState state)
        {
            _position = position;
            _state = state;
        }

        public void ToList(CellList list)
        {
            list.Cells.Add(new Cell(list, _position, _state));
        }
    }

    public class KillUnderpoulatedCell : ExecuteRuleOnCell
    {
        public override bool TryExecute(Cell cell, out AddNewCellToList resultingCell)
        {
            if (CountNeighbours(cell) < 2)
            {
                resultingCell = new AddNewCellToList(cell.Point, CellState.Dead);
                return true;
            }
            resultingCell = null;
            return false;
        }
    }

    public class FindCellAtPosition
    {
        public Cell Find(CellList list, Point position)
        {
            return list.Cells.FirstOrDefault(c => c.Point == position);
        }
    }

    public class FindNeighboursOfCellAtPosition
    {
        public IEnumerable<Cell> Find(CellList list, Point position)
        {
            var generator = new GeneratePointsAroundPoint();

            IEnumerable<Point> points = generator.Generate(position);

            var cellFinder = new FindCellAtPosition();

            return points.Select(p => cellFinder.Find(list, p)).Where(c => c != null);
        }
    }

    public class GeneratePointsAroundPoint
    {
        public IEnumerable<Point> Generate(Point point)
        {
            var offsets = new[] {-1, 0, 1};

            foreach (int xOffset in offsets)
            {
                foreach (int yOffset in offsets)
                {
                    if (xOffset == 0 && yOffset == 0) continue;
                    yield return Point.OffsetFrom(point, xOffset, yOffset);
                }
            }
        }
    }

    public class CellList
    {
        public CellList()
        {
            Cells = new List<Cell>();
        }

        public List<Cell> Cells { get; set; }
    }

    public class Cell
    {
        public Cell(CellList list, Point point, CellState state)
        {
            Point = point;
            State = state;
            List = list;
        }

        public CellList List { get; private set; }
        public Point Point { get; private set; }
        public CellState State { get; private set; }
    }

    public abstract class CellState
    {
        public static readonly CellState Dead = new DeadCellState();
        public static readonly CellState Live = new LiveCellState();
        public abstract bool IsAlive { get; }
        public abstract bool IsDead { get; }
    }

    public class DeadCellState : CellState
    {
        public override bool IsAlive
        {
            get { return false; }
        }

        public override bool IsDead
        {
            get { return true; }
        }
    }

    public class LiveCellState : CellState
    {
        public override bool IsAlive
        {
            get { return true; }
        }

        public override bool IsDead
        {
            get { return false; }
        }
    }

    public class Point : IEquatable<Point>
    {
        readonly int _x;
        readonly int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x*397) ^ _y;
            }
        }

        public static bool operator ==(Point left, Point right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !Equals(left, right);
        }

        public static Point OffsetFrom(Point point, int xOffset, int yOffset)
        {
            return new Point(point._x + xOffset, point._y + yOffset);
        }
    }
}