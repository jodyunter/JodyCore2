﻿using JodyCore2.Data;
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

        public override TeamDto SetupCreateData(JodyContext context)
        {
            return new TeamDto(Guid.NewGuid(), "My Team", 25);
        }

        public override TeamDto SetupUpdateData(TeamDto originalData, JodyContext context)
        {
            var updatedData = Repository.GetByIdentifier(originalData.Identifier, context).First();
            updatedData.Name = "New Name";
            updatedData.Skill = 50;

            return updatedData;
        }

        public override IList<TeamDto> SetupGetAllData(JodyContext context)
        {
            return SetupGenericTeams(10, context, teamRepository);            
        }

        public override IBaseRepository<TeamDto> SetupRepository()
        {
            teamRepository = new TeamRepository();
            return teamRepository;
        }

        [Test]
        public void ShouldGetByName()
        {
            //setup data
            using (var context = new JodyContext())
            {
                SetupGenericTeams(10, context, teamRepository);

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

        [Test]
        public void ShouldNotDeleteTeamWithGames()
        {
            Assert.Fail();
        }

        public static IList<TeamDto> SetupGenericTeams(int count, JodyContext context, ITeamRepository teamRepository)
        {
            var list = new List<TeamDto>();

            for (int i = 0; i < count; i++)
            {
                var teamDto = new TeamDto(Guid.NewGuid(), "Team " + i, i);
                list.Add(teamRepository.Create(teamDto, context));
            }

            return list;
        }

    }
}
