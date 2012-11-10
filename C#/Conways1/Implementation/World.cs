using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Conways1
{
    public class World
    {
        readonly List<Cell> cells = new List<Cell>();
        readonly ConcurrentDictionary<Position, bool> memoizedLiveCells = new ConcurrentDictionary<Position, bool>();
        readonly ConcurrentDictionary<Position, int> memoizedLiveNeighbourCounts = new ConcurrentDictionary<Position, int>();

        public int TotalCells
        {
            get { return cells.Count; }
        }

        public void AddLiveCell(Position position)
        {
            Cell cell = Cell.LiveAt(position);
            cells.Add(cell);
            memoizedLiveCells[position] = cell.IsAlive;
        }

        public int GetLiveNeighbours(Position position)
        {
            return memoizedLiveNeighbourCounts.GetOrAdd(position, CalculateLiveNeighbours);
        }

        public bool IsCellAlive(Position position)
        {
            bool isAlive;

            if (memoizedLiveCells.TryGetValue(position, out isAlive))
            {
                return isAlive;
            }

            return false;
        }

        public bool IsCellDead(Position position)
        {
            return !IsCellAlive(position);
        }

        public World Tick()
        {
            var newWorld = new World();

            foreach (Cell cell in GetRemainingLiveCellsAfterTick().Concat(GetNewLiveCellsAfterTick()))
            {
                newWorld.AddLiveCell(cell.Position);
            }

            return newWorld;
        }

        IEnumerable<Cell> GetNewLiveCellsAfterTick()
        {
            var positions = new ConcurrentDictionary<Position, bool>();

            return cells.AsParallel().Where(cell => cell.IsAlive).SelectMany(cell => ReanimateDeadNeighbours(cell, positions));
        }

        int CalculateLiveNeighbours(Position position)
        {
            int live = 0;

            VisitNeighbours(position, neighbourPosition =>
                {
                    bool match = IsCellAlive(neighbourPosition);
                    if (match) live++;
                });

            return live;
        }

        void VisitNeighbours(Position position, Action<Position> visitor)
        {
            foreach (Position neighbourPosition in position.GetNeighbours())
            {
                visitor(neighbourPosition);
            }
        }

        IEnumerable<Cell> ReanimateDeadNeighbours(Cell cell, ConcurrentDictionary<Position, bool> positionsWeAlreadyChecked)
        {
            var newCells = new List<Cell>();

            VisitNeighbours(cell.Position, position =>
                {
                    if (HaveVisitedAlready(positionsWeAlreadyChecked, position)) return;

                    Cell newLiveCell;

                    if (TryBringCellBackToLife(position, out newLiveCell))
                    {
                        newCells.Add(newLiveCell);
                    }
                });

            return newCells;
        }

        static bool HaveVisitedAlready(ConcurrentDictionary<Position, bool> positionsWeAlreadyChecked, Position position)
        {
            bool wasAdded = false;

            positionsWeAlreadyChecked.GetOrAdd(position, p =>
                {
                    wasAdded = true;
                    return true;
                });

            if (!wasAdded) return true;
            return false;
        }

        bool TryBringCellBackToLife(Position position, out Cell newLiveCell)
        {
            Cell deadCell = Cell.DeadAt(position);

            int liveNeighbourCount = GetLiveNeighbours(position);

            newLiveCell = deadCell.Tick(liveNeighbourCount);

            return newLiveCell.IsAlive;
        }

        IEnumerable<Cell> GetRemainingLiveCellsAfterTick()
        {
            return cells.AsParallel().Select(cell => cell.Tick(GetLiveNeighbours(cell.Position))).Where(cell => cell.IsAlive);
        }
    }
}