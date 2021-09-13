using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Games
{
    public class TestGame
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

        [Fact]
        public void ShouldGetWinner()
        {
            var game = new Game(Guid.NewGuid(), 5, 25, new Team(Guid.NewGuid(), "Team 1", 5), new Team(Guid.NewGuid(), "Team 2", 5), 0, 0, false, false, true);
            game.Play(RandomUtility.GetRandom(5505505));
            game.HomeScore = 12;
            game.AwayScore = 6;

            var winner = game.GetWinner();

            Assert.Equal("Team 1", winner.Name);

            game.HomeScore = 3;

            winner = game.GetWinner();

            Assert.Equal("Team 2", winner.Name);

        }

        [Fact]
        public void ShouldGetLoser()
        {
            var game = new Game(Guid.NewGuid(), 5, 25, new Team(Guid.NewGuid(), "Team 1", 5), new Team(Guid.NewGuid(), "Team 2", 5), 0, 0, false, false, true);
            game.Play(RandomUtility.GetRandom(5505505));
            game.HomeScore = 12;
            game.AwayScore = 6;

            var loser = game.GetLoser();

            Assert.Equal("Team 2", loser.Name);

            game.HomeScore = 3;

            loser = game.GetLoser();

            Assert.Equal("Team 1", loser.Name);

        }
    }


}
