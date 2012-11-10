namespace Conways1
{
    public class TickCountingConwayModel : IConwayModel
    {
        public int Ticks { get; set; }

        public bool IsLive(int x, int y)
        {
            return false;
        }

        public void Tick()
        {
            Ticks++;
        }

        public int TotalCells
        {
            get { return 0; }
        }
    }
}