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
    public class TestTeamService
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
            teamService = new TeamService(new TeamRepository());
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
        public void ShouldUpdateTeam()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldGetAll()
        {
            Assert.Fail();
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
    }
}
