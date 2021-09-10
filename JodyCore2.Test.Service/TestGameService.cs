using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Standing;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using JodyCore2.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Service
{
    public class TestGameService:BaseIntegrationTest
    {
        IGameService gameService;

        [SetUp]
        public void Setup()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            gameService = new GameService(new TeamRepository(), new GameRepository(), new StandingsRepository());
        }

        [Test]
        public void ShouldCreateGame()
        {
            var teamRepo = new TeamRepository();
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 6);

            using (var context = new JodyContext())
            {
                teamRepo.Create(team1, context);
                teamRepo.Create(team2, context);
                context.SaveChanges();
            }

            var model = gameService.Create(12, 1, team1.Identifier, team2.Identifier);

            Assert.NotNull(model.Identifier);
            Assert.AreEqual(model.Day, 1);
            Assert.AreEqual(model.Year, 12);
            Assert.AreEqual(model.HomeScore, 0);
            Assert.AreEqual(model.AwayScore, 0);
            Assert.IsFalse(model.Complete);
            Assert.AreEqual(model.HomeTeamIdentifier, team1.Identifier);
            Assert.AreEqual(model.AwayTeamIdentifier, team2.Identifier);
        }

        [Test]
        public void ShouldNotCreateHomeTeamDoesNotExist()
        {
            var teamRepo = new TeamRepository();
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 6);

            using (var context = new JodyContext())
            {                
                teamRepo.Create(team2, context);
                context.SaveChanges();
            }
            
            var e = Assert.Throws<ApplicationException>(() => gameService.Create(12, 1, team1.Identifier, team2.Identifier));
            Assert.AreEqual(string.Format("Home or Away Team Does not exist. Home identifier is {0}.  Away Identifier is {1}", team1.Identifier, team2.Identifier), e.Message);            
        }

        [Test]
        public void ShouldNotCreateAwyTeamDoesNotExist()
        {
            var teamRepo = new TeamRepository();
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 6);

            using (var context = new JodyContext())
            {
                teamRepo.Create(team1, context);
                context.SaveChanges();
            }

            var e = Assert.Throws<ApplicationException>(() => gameService.Create(12, 1, team1.Identifier, team2.Identifier));
            Assert.AreEqual(string.Format("Home or Away Team Does not exist. Home identifier is {0}.  Away Identifier is {1}", team1.Identifier, team2.Identifier), e.Message);
        }
        [Test]
        public void ShouldUpdateGame()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldGetGames()
        {
            var teamList = new List<Team>();

            using (var context = new JodyContext())
            {


                for (int i = 0; i < 10; i++)
                {
                    teamList.Add(new Team(Guid.NewGuid(), "Team " + i, 5));                    
                }

                var repo = new TeamRepository();
                repo.Create(teamList, context);

                context.SaveChanges();

            }

            gameService.Create(2, 1, teamList[0].Identifier, teamList[1].Identifier);
            gameService.Create(2, 2, teamList[2].Identifier, teamList[1].Identifier);
            gameService.Create(2, 3, teamList[4].Identifier, teamList[1].Identifier);
            gameService.Create(2, 4, teamList[6].Identifier, teamList[1].Identifier);
            gameService.Create(2, 5, teamList[8].Identifier, teamList[1].Identifier);

            var games = gameService.GetGames(2, 1, 12);

            Assert.AreEqual(5, games.Count());
        }

        [Test]
        public void ShouldPlayGame()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldNotPlayCompleteGame()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldNotPlayGameGameIdDoesNotExist()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldNotPlayGameSameTeams()
        {
            Assert.Fail();
        }

        
    }

}
