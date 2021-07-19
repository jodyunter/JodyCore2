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
        IGameRepository gameRepository;

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

        public override IList<TeamDto> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }
        public override IList<TeamDto> SetupGetAllData(JodyContext context)
        {
            return SetupGenericTeams(10, context, teamRepository);            
        }

        public override IBaseRepository<TeamDto> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository = new GameRepository();
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

        //this should be moved to the service layer or else we're just testing the FK contstraint
        [Test]
        public void ShouldNotDeleteTeamWithGames()
        {
            var team1 = new TeamDto(Guid.NewGuid(), "Team 1", 5);
            var team2 = new TeamDto(Guid.NewGuid(), "Team 2", 5);
            var team3 = new TeamDto(Guid.NewGuid(), "Team 3", 5);

            var game = new GameDto(Guid.NewGuid(), 1, 5, team1, team3, 5, 5, true, false, true);

            using (var context = new JodyContext())
            {
                teamRepository.Create(new List<TeamDto>() { team1, team2, team3 }, context);                                           
                gameRepository.Create(game, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                teamRepository.Delete(team1, context);
                var e = Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => context.SaveChanges());
            }            
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
