using System;
using System.Text;

namespace ConwayConsole
{
    public class ConwayTerminalDisplay
    {
        readonly IConwayModel _model;
        readonly int height;
        readonly int width;
        bool? forceTick;
        int offsetX;
        int offsetY;

        public ConwayTerminalDisplay(int width, int height, IConwayModel model)
        {
            this.width = width;
            this.height = height;
            _model = model;
        }

        public void Start()
        {
            ConfigureConsole();

            Redraw();

            while (true)
            {
                bool requiresRedraw = false;

                if (forceTick == null)
                {
                    requiresRedraw = _model.Tick();
                }
                else if (forceTick == true)
                {
                    requiresRedraw = _model.Tick();
                    forceTick = false;
                }

                if (requiresRedraw)
                {
                    Redraw();
                }

                if (ProcessKeyboardInput())
                {
                    Redraw();
                }
            }
        }

        void Redraw()
        {
            OutputBoard();

            OutputStatusLine();
        }

        void ConfigureConsole()
        {
            Console.Clear();

            Console.SetWindowSize(width, height + 1);
            Console.BufferWidth = width;
            Console.BufferHeight = height + 1;
            Console.CursorVisible = false;
        }

        void OutputBoard()
        {
            for (int y = 0; y < height; y++)
            {
                var line = new StringBuilder();

                for (int x = 0; x < width; x++)
                {
                    bool isAlive = _model.IsLive(x + offsetX, y + offsetY);

                    line.Append(isAlive ? "#" : " ");
                }

                Console.SetCursorPosition(0, y);

                Console.Write(line.ToString());
            }
        }

        void OutputStatusLine()
        {
            Console.SetCursorPosition(0, height);
            Console.WriteLine("Total Cells: {0}, Ticks: {1}, X/Y: {2},{3}", _model.TotalCells, _model.Ticks, offsetX, offsetY);
        }

        bool ProcessKeyboardInput()
        {
            if (Console.KeyAvailable)
            {
                bool processedOutput = true;

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.T:
                        forceTick = true;
                        break;
                    case ConsoleKey.R:
                        forceTick = null;
                        break;
                    case ConsoleKey.LeftArrow:
                        offsetX--;
                        break;
                    case ConsoleKey.RightArrow:
                        offsetX++;
                        break;
                    case ConsoleKey.UpArrow:
                        offsetY--;
                        break;
                    case ConsoleKey.DownArrow:
                        offsetY++;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Spacebar:
                        offsetX = 0;
                        offsetY = 0;
                        break;
                    default:
                        processedOutput = false;
                        break;
                }

                return processedOutput;
            }

            return false;
        }
    }
}