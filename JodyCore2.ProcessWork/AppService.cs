using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Rankings;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ProcessWork
{
    public class AppService
    {
        public ITeamService TeamService { get; set; }
        public IRankingService RankingService { get; set; }
        public ITeamRepository TeamRepository { get; set; }
        public IGameRepository GameRepository { get; set; }
        public ICompetitionRepository CompetitionRepository { get; set; }
        public IRankingGroupRepository RankingGroupRepository { get; set; }
        

        void SetupServices()
        {
            TeamService = new TeamService(TeamRepository, GameRepository);
            RankingService = new RankingService(RankingGroupRepository, CompetitionRepository, TeamRepository);
        }
        void SetupRepos()
        {
            TeamRepository = new TeamRepository();
            GameRepository = new GameRepository();
            CompetitionRepository = new CompetitionRepository();
            RankingGroupRepository = new RankingGroupRepository();
        }

        public AppService()
        {
            SetupRepos();
            SetupServices();
        }
    }
}
