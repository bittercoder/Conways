using ConwayConsole;

namespace Conways2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new CellList();

            string[] setup = ConwayBoards.Acorn();

            list.SetupCells(setup);

            var model = new CellListToConwayModelWrapper(list);
            
            var display = new ConwayTerminalDisplay(79, 35, model);

            display.Start();
        }
    }
}