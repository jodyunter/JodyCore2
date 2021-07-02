using NUnit.Framework;
using JodyCore2.Domain.Bo;
using System;
using JodyCore2.Service.Util;

namespace JodyCore2.Test.Domain
{
    public class TestGame
    {
        [Test]
        public void ShouldPlayGame()
        {
            var game = new Game(Guid.NewGuid(), 5, 25, new Team(Guid.NewGuid(), "Team 1", 5), new Team(Guid.NewGuid(), "Team 2", 5), 0, 0, false, false, true);

            game.Play(RandomUtility.GetRandom(1277734512));

            Assert.IsTrue(game.Complete);
            Assert.AreEqual(3, game.HomeScore);
            Assert.AreEqual(5, game.AwayScore);

        }
    }
}
