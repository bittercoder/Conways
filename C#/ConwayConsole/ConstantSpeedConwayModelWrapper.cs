using System;

namespace ConwayConsole
{
    public class ConstantSpeedConwayModelWrapper : IConwayModel
    {
        readonly IClock clock;
        readonly IConwayModel innerModel;
        DateTime? lastTimeWeTicked;

        public ConstantSpeedConwayModelWrapper(IConwayModel innerModel, IClock clock)
        {
            this.innerModel = innerModel;
            this.clock = clock;
        }

        public double TicksPerSecond { get; set; }

        public bool IsLive(int x, int y)
        {
            return innerModel.IsLive(x, y);
        }

        public bool Tick()
        {
            DateTime now = clock.Now;

            if (lastTimeWeTicked == null)
            {
                lastTimeWeTicked = now;
                innerModel.Tick();
                return true;
            }

            double seconds = now.Subtract(lastTimeWeTicked.Value).TotalSeconds;

            double ticks = Math.Floor(TicksPerSecond*seconds);

            if (ticks > 0)
            {
                for (int i = 0; i < ticks; i++) innerModel.Tick();
                lastTimeWeTicked = now;
                return true;
            }

            return false;
        }

        public int TotalCells
        {
            get { return innerModel.TotalCells; }
        }

        public int Ticks
        {
            get { return innerModel.Ticks; }
        }
    }
}