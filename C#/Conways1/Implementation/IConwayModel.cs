namespace Conways1
{
    public interface IConwayModel
    {
        int TotalCells { get; }
        int Ticks { get; }
        bool IsLive(int x, int y);
        void Tick();
    }
}