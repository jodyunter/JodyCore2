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
    public class TestTeamRepository:TestBaseRepository<TeamDto>
    {
        ITeamRepository teamRepository;

        public override IBaseRepository<TeamDto> SetupRepository()
        {
            teamRepository = new TeamRepository();
            return teamRepository;
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

                teamRepository.Create(teamDto, context);

                context.SaveChanges();

                var newTeam = teamRepository.GetByIdentifier(teamDto.Identifier, context);

                Assert.AreEqual(name, newTeam.Name);
                Assert.AreEqual(identifier, newTeam.Identifier);
                Assert.AreEqual(skill, newTeam.Skill);
            }
        }

        [Test]
        public void ShouldUpdateTeam()
        {
            using (var context = new JodyContext())
            {
                var identifier = Guid.NewGuid();
                var name = "My Name";
                var skill = 25;

                var teamDto = new TeamDto(identifier, name, skill);

                teamRepository.Create(teamDto, context);

                context.SaveChanges();

                teamDto.Name = "New Name";
                teamDto.Skill = 250;

                teamRepository.Update(teamDto, context);

                context.SaveChanges();

                var updatedTeam = teamRepository.GetByIdentifier(teamDto.Identifier, context);

                Assert.AreEqual("New Name", updatedTeam.Name);
                Assert.AreEqual(teamDto.Identifier, updatedTeam.Identifier);
                Assert.AreEqual(250, updatedTeam.Skill);

            }
        }

        [Test]
        public void ShouldGetAll()
        {
            using (var context = new JodyContext())
            {
                SetupGenericTeams(10, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var teams = teamRepository.GetAll(context);

                Assert.AreEqual(10, teams.Count);
                Assert.AreEqual(context.Teams.Count(), teams.Count);
            }

        }

        [Test]
        public void ShouldGetByName()
        {
            //setup data
            using (var context = new JodyContext())
            {
                SetupGenericTeams(10, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var team = teamRepository.GetByName("Team 5", context);

                Assert.AreEqual("Team 5", team.Name);

                team = teamRepository.GetByName("Team 0", context);
                Assert.AreEqual("Team 0", team.Name);
            }
        }

        void SetupGenericTeams(int count, JodyContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var teamDto = new TeamDto(Guid.NewGuid(), "Team " + i, i);
                teamRepository.Create(teamDto, context);
            }
        }

    }
}
