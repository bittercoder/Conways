using System;

namespace ConwayConsole
{
    public class FixedClock : IClock
    {
        public DateTime Now { get; set; }
    }

    public interface IClock
    {
        DateTime Now { get; }
    }

    public class SystemClock : IClock
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}