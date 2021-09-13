using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Rankings;
using JodyCore2.Data.Repositories.Standing;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp
{
    public class Services
    {
        public ITeamService TeamService { get; set; }
        public IGameService GameService { get; set; }
        public ISchedulingService SchedulingService { get; set; }
        public IStandingsService StandingsService { get; set; }

        public Services()
        {
            var teamRepository = new TeamRepository();
            var gameRepository = new GameRepository();
            var standingsRepository = new StandingsRepository();
            var rankingGroupRepository = new RankingGroupRepository();

            var competitionGameRepository = new CompetitionGameRepository();
            var competitionRankingGroupRepository = new CompetitionRankingGroupRepository();

            TeamService = new TeamService(teamRepository, gameRepository);
            GameService = new GameService(teamRepository, gameRepository);
            SchedulingService = new SchedulingService();
            StandingsService = new StandingsService(standingsRepository, teamRepository, competitionGameRepository, competitionRankingGroupRepository);
        }
        
    }
}
