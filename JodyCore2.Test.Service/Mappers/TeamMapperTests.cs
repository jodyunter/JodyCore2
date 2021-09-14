using JodyCore2.Domain.Bo;
using JodyCore2.Service.Mappers;
using NUnit.Framework;
using System;

namespace JodyCore2.Test.Service.Mappers
{
    public class TeamMapperTests
    {
        [Test]
        public void ShouldMapTeamViewModelToTeam()
        {
            var team = new Team(Guid.NewGuid(), "My Name", 25);

            var model = TeamMapper.TeamToTeamViewModel(team);

            Assert.AreEqual(team.Identifier, model.Identifier);
            Assert.AreEqual(team.Name, model.Name);
            Assert.AreEqual(team.Skill, model.Skill);
        }

    }
}
