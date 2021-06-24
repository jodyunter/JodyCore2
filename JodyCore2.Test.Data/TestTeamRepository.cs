using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Data
{
    public class TestTeamRepository
    {
        [SetUp]
        public void Setup()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void ShouldCreateTeam()
        {
            using (var context = new JodyContext())
            {
                var identifier = Guid.NewGuid();
                var name = "My Name";
                var skill = 25;

                var teamDto = new TeamDto(identifier, name, skill);

                ITeamRepository teamRepository = new TeamRepository();

                teamRepository.Create(teamDto, context);

                context.SaveChanges();

                var newTeam = context.Teams.Where(t => t.Name == name).FirstOrDefault();

                Assert.AreEqual(name, newTeam.Name);
                Assert.AreEqual(identifier, newTeam.Identifier);
                Assert.AreEqual(skill, newTeam.Skill);
            }
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
    }
}
