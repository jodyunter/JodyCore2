using JodyCore2.Domain.Bo;
using JodyCore2.Service.Mappers;
using NUnit.Framework;
using System;

namespace JodyCore2.Test.Service.Mappers
{
    public class GameMapperTests
    {
        [Test]
        public void ShouldMapGameToGameSummaryViewModel()
        {
            var home = new Team(Guid.NewGuid(), "Team 1", 6);
            var away = new Team(Guid.NewGuid(), "Team 2", 12);

            var game = new Game(Guid.NewGuid(), 5, 200, home, away, 55, 600, false, true, false);

            var model = GameMapper.GameToGameSummaryViewModel(game);

            Assert.AreEqual(game.Identifier, model.Identifier);
            Assert.AreEqual(game.Day, model.Day);
            Assert.AreEqual(game.Year, model.Year);
            Assert.AreEqual(game.Home.Identifier, model.HomeTeamIdentifier);
            Assert.AreEqual(game.Home.Name, model.HomeTeamName);
            Assert.AreEqual(game.HomeScore, model.HomeScore);
            Assert.AreEqual(game.Away.Identifier, model.AwayTeamIdentifier);
            Assert.AreEqual(game.Away.Name, model.AwayTeamName);
            Assert.AreEqual(game.AwayScore, model.AwayScore);
            Assert.AreEqual(game.Complete, model.Complete);

        }
    }
}
