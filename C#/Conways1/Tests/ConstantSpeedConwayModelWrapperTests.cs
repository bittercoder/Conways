using System;
using ConwayConsole;
using Xunit;

namespace Conways1
{
    public class ConstantSpeedConwayModelWrapperTests
    {
        [Fact]
        public void when_ticks_per_second_is_set_30_and_advance_by_1_second_then_30_ticks_should_occur()
        {
            var tickCounter = new TickCountingConwayModel();

            var clock = new FixedClock {Now = new DateTime(2012, 1, 1, 1, 0, 0)};

            var model = new ConstantSpeedConwayModelWrapper(tickCounter, clock) {TicksPerSecond = 30};

            model.Tick();

            tickCounter.Ticks = 0;

            clock.Now = clock.Now.AddSeconds(1);

            model.Tick();

            Assert.Equal(30, tickCounter.Ticks);
        }
    }
}