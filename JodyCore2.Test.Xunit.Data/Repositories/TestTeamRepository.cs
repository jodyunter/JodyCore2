using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories
{
    public class TestTeamRepository:TestBaseRepository<Team>
    {
        ITeamRepository teamRepository;
        IGameRepository gameRepository;

        public override Team SetupCreateData(JodyContext context)
        {
            return new Team(Guid.NewGuid(), "My Team", 25);
        }

        public override Team SetupUpdateData(Team originalData, JodyContext context)
        {
            var updatedData = Repository.GetByIdentifier(originalData.Identifier, context).First();
            updatedData.Name = "New Name";
            updatedData.Skill = 50;

            return updatedData;
        }

        public override IList<Team> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }
        public override IList<Team> SetupGetAllData(JodyContext context)
        {
            return SetupGenericTeams(10, context, teamRepository);
        }

        public override IBaseRepository<Team> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository = new GameRepository();
            return teamRepository;
        }
        public static IList<Team> SetupGenericTeams(int count, JodyContext context, ITeamRepository teamRepository)
        {
            var list = new List<Team>();

            for (int i = 0; i < count; i++)
            {
                var teamDto = new Team(Guid.NewGuid(), "Team " + i, i);
                list.Add(teamRepository.Create(teamDto, context));
            }

            return list;
        }
    }
}
