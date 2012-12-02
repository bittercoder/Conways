namespace ConwayConsole
{
    public class TickCountingConwayModel : IConwayModel
    {
        public int Ticks { get; set; }

        public bool IsLive(int x, int y)
        {
            return false;
        }

        public bool Tick()
        {
            Ticks++;
            return true;
        }

        public int TotalCells
        {
            get { return 0; }
        }
    }
}