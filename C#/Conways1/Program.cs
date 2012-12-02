using ConwayConsole;

namespace Conways1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            string[] setup = ConwayBoards.Puffer();

            world.SetupWorld(setup);

            var model = new WorldToConwayModelWrapper(world);

            // you can use the constant speed model if you want things to happen at a specific pace - useful
            // if the technology being used to render does not take long - for us, console is the bottle neck.

            //var clock = new SystemClock();
            //var constantModel = new ConwayConstantSpeedModel(model, clock) {TicksPerSecond = 3};

            var display = new ConwayTerminalDisplay(79, 35, model);

            display.Start();
        }
    }
}