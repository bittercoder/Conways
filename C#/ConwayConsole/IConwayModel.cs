namespace ConwayConsole
{
    public interface IConwayModel
    {
        int TotalCells { get; }
        int Ticks { get; }
        bool IsLive(int x, int y);
        bool Tick();
    }
}