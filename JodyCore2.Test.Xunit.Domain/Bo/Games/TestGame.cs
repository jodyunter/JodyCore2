using JodyCore2.Domain.Bo;
using JodyCore2.Service.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Games
{
    public class GameTests
    {
        [Fact]
        public void ShouldPlayGame()
        {
            var game = new Game(Guid.NewGuid(), 5, 25, new Team(Guid.NewGuid(), "Team 1", 5), new Team(Guid.NewGuid(), "Team 2", 5), 0, 0, false, false, true);

            game.Play(RandomUtility.GetRandom(1277734512));

            Assert.True(game.Complete);
            Assert.StrictEqual(3, game.HomeScore);
            Assert.StrictEqual(5, game.AwayScore);

        }
    }
}
