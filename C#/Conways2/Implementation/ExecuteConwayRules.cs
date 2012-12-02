using System.Collections.Generic;
using System.Linq;

namespace Conways2.Tests
{
    public class ExecuteConwayRules
    {
        static readonly ExecuteRuleOnCell[] rules = new ExecuteRuleOnCell[] { new KillUnderpoulatedCell(), new KillOverpopulatedCell(), new ResurrectCellThroughReproduction() };

        public CellList GetNextGeneration(CellList list)
        {
            var cellsToEvaluate = new EnumerateCellsAndAddMissingDeadNeighbours().Enumerate(list);

            IEnumerable<AddNewCellToList> results = cellsToEvaluate.Select(EvaluateNextGenerationOfCell);

            return ApplyNextGenerationResultsToNewList(results);
        }

        static CellList ApplyNextGenerationResultsToNewList(IEnumerable<AddNewCellToList> results)
        {
            var nextGeneration = new CellList();

            foreach (AddNewCellToList result in results) result.ToList(nextGeneration);
            
            return nextGeneration;
        }

        static AddNewCellToList EvaluateNextGenerationOfCell(Cell cell)
        {
            foreach (var rule in rules)
            {
                AddNewCellToList resultingCell;
                if (rule.TryExecute(cell, out resultingCell)) return resultingCell;
            }

            return new AddNewCellToList(cell.Point, cell.State);
        }
    }
}