using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Service
{
    public class TestTeamService:BaseIntegrationTest
    {
        ITeamService teamService;
        
        [SetUp]
        public void Setup()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();                
            }
            teamService = new TeamService(new TeamRepository(), new GameRepository());
        }
        [Test]
        public void ShouldCreateTeam()
        {
            var newTeam = teamService.Create("My Team", 25);

            var retrievedTeam = teamService.GetByIdentifier(newTeam.Identifier);

            Assert.AreEqual("My Team", retrievedTeam.Name);
            Assert.AreEqual(25, retrievedTeam.Skill);
            Assert.NotNull(newTeam.Identifier);
            Assert.AreEqual(newTeam.Identifier, retrievedTeam.Identifier);
        }

        [Test]        
        public void ShouldNotCreateTeamNameInUse()
        {
            var newTeam = teamService.Create("Team 5", 50);

            var e = Assert.Throws<ApplicationException>(() => teamService.Create("Team 5", 25));
            Assert.AreEqual("Team with name Team 5 already exists.", e.Message);


        }

        [Test]
        public void ShouldUpdateTeam()
        {
            var newTeam = teamService.Create("My Team", 25);

            teamService.Save(newTeam.Identifier, "new Name", 55);

            var retrievedTeam = teamService.GetByName("new Name");

            Assert.AreEqual("new Name", retrievedTeam.Name);
            Assert.AreEqual(55, retrievedTeam.Skill);
            Assert.NotNull(newTeam.Identifier);
            Assert.AreEqual(newTeam.Identifier, retrievedTeam.Identifier);
        }

        [Test]
        public void ShouldGetAll()
        {
            for (int i = 0; i < 10; i++) 
            {
                teamService.Create("Team " + i, 25);
            }

            var teams = teamService.GetAll();

            Assert.AreEqual(10, teams.Count);
        }

        [Test]
        public void ShouldGetTeamByName()
        {
            var newTeam = teamService.Create("My Team", 25);

            var retrievedTeam = teamService.GetByName("My Team");

            Assert.AreEqual("My Team", retrievedTeam.Name);
            Assert.AreEqual(25, retrievedTeam.Skill);
            Assert.NotNull(newTeam.Identifier);
            Assert.AreEqual(newTeam.Identifier, retrievedTeam.Identifier);
        }

        [Test]
        public void ShouldGetTeamByIdentifier()
        {
            var newTeam = teamService.Create("My Team", 25);

            var retrievedTeam = teamService.GetByIdentifier(newTeam.Identifier);

            Assert.AreEqual("My Team", retrievedTeam.Name);
            Assert.AreEqual(25, retrievedTeam.Skill);
            Assert.NotNull(newTeam.Identifier);
            Assert.AreEqual(newTeam.Identifier, retrievedTeam.Identifier);
        }

        [Test]
        public void ShouldDeleteTeam()
        {
            var newTeam = teamService.Create("Test Team", 25);

            var retreivedTeam = teamService.GetByIdentifier(newTeam.Identifier);

            teamService.Delete(newTeam.Identifier);

            var e = Assert.Throws<ApplicationException>(() => teamService.GetByIdentifier(newTeam.Identifier));
            Assert.AreEqual(string.Format("Team with identifier {0} does not exist.", newTeam.Identifier), e.Message);
        }

        [Test]
        public void ShouldNotDeleteTeamDoesNotExist()
        {
            var id = Guid.NewGuid();

            var e = Assert.Throws<ApplicationException>(() => teamService.Delete(id));
            Assert.AreEqual(string.Format("Team with identifier {0} does not exist.", id), e.Message);
        }

        [Test]
        public void ShouldNotDeleteGamesExist()
        {
            var team1 = teamService.Create("Team 1", 5);
            var team2 = teamService.Create("Team 2", 5);

            var gameService = new GameService(new TeamRepository(), new GameRepository());

            gameService.Create(1, 1, team1.Identifier, team2.Identifier);

            var e = Assert.Throws<ApplicationException>(() => teamService.Delete(team1.Identifier));
            Assert.AreEqual(string.Format("Games with Team {0} exist. Cannot delete.", team1.Identifier), e.Message);

            var e2 = Assert.Throws<ApplicationException>(() => teamService.Delete(team2.Identifier));
            Assert.AreEqual(string.Format("Games with Team {0} exist. Cannot delete.", team2.Identifier), e2.Message);

        }

    }
}
