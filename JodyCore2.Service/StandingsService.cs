using JodyCore2.Data;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using JodyCore2.Domain.Bo.Rankings;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Standing;

namespace JodyCore2.Service
{
    public class StandingsService : IStandingsService
    {
        private readonly IStandingsRepository standingsRepository;
        private readonly ITeamRepository teamRepository;
        private readonly ICompetitionGameRepository gameRepository;
        private readonly ICompetitionRankingGroupRepository rankingGroupRepository;

        public StandingsService(IStandingsRepository _standingsRepository, ITeamRepository _teamRepository, ICompetitionGameRepository _gameRepository, ICompetitionRankingGroupRepository _rankingGroupRepository)
        {
            standingsRepository = _standingsRepository;
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
            rankingGroupRepository = _rankingGroupRepository;
        }

        //todo eventually this will come from a configuration item that is already stored in the database
        public IStandingsViewModel Create(string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<ITeamViewModel> teamsToInclude)
        {
            //todo CHECSK ON NAME ETC
            using (var context = new JodyContext()) 
            {
                var standings = new Standings(Guid.NewGuid(), name, startYear, startDay, 1, description, division, null, false, false, false, false);

                var teamDtoList = teamsToInclude.Select(t => teamRepository.GetByIdentifier(t.Identifier, context).FirstOrDefault()).ToList();
                var standingsRecordList = teamDtoList.Select(ta => new StandingsRecord(Guid.NewGuid(), standings, ta, ta.Name, 0, 0, 0, 0, 0, 0, 0, 0, 0)).ToList();

                standings.Records = standingsRecordList.ToList<IStandingsRecord>();

                context.Add(standings);
                //create the default rankings
                var rankings = new CompetitionRankingGroup(Guid.NewGuid(), standings, division, new List<ICompetitionRanking>());
                teamDtoList.ForEach(team =>
                {
                    rankings.Rankings.Add(new CompetitionRanking(Guid.NewGuid(), team, 1, rankings));
                });

                context.Add(rankings);

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings, rankings);
            }
        }

        public IStandingsViewModel Sort(Guid guid)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault();
                //todo add a get by standings method
                var rankingGroup = rankingGroupRepository.GetByCompetition(standings.Identifier, context).FirstOrDefault();

                var sortedRecords = standings.Records.ToList().OrderByDescending(r => r.Points)
                            .ThenBy(r => r.GamesPlayed)
                            .ThenByDescending(r => r.Wins)
                            .ThenByDescending(r => r.GoalDifference)
                            .ThenByDescending(r => r.GoalsFor).ToList();

                for (int i = 0; i < sortedRecords.Count(); i++)
                {
                    var ranking = rankingGroup.GetRankingByTeam(sortedRecords[i].Team);
                    ranking.SetRank(i + 1);
                }

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings, rankingGroup);
            }
        }

        public IStandingsViewModel GetByIdentifier(Guid guid)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault();
                var rankingGroup = rankingGroupRepository.GetAll(context).Where(rg => rg.Competition.Identifier == standings.Identifier).FirstOrDefault();
                return StandingsMapper.StandingsToStandingsViewModel(standings, rankingGroup);
            }            
        }

        //Process Games will process all incomplete and unprocessed games
        public IStandingsViewModel ProcessGames(Guid standingsIdentifier)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(standingsIdentifier, context)).FirstOrDefault();
                //todo make this simpler
                var gameDtos = gameRepository.GetByCompetition(standings.Identifier, context).Where(g => !g.Processed && g.Complete);
                
                               
                gameDtos.ToList().ForEach(g =>
                {
                    standings.ProcessGame(g);
                });

                context.SaveChanges();

                var rankingGroup = rankingGroupRepository.GetByCompetition(standings.Identifier, context).FirstOrDefault();

                return StandingsMapper.StandingsToStandingsViewModel(standings, rankingGroup);
            }
        }
        public IList<IGameSummaryViewModel> GetStandingsGames(Guid standingsIdentifier, int year, int firstDay, int lastDay)
        {
            using (var context = new JodyContext())
            {
                return gameRepository.GetByYearAndDayRange(year, firstDay, lastDay, context).Where(g => g.Competition.Identifier == standingsIdentifier).Select(g => GameMapper.GameToGameSummaryViewModel(g)).ToList();
            }
        }

        public IGameSummaryViewModel CreateStandingsGame(Guid standingsIdentifier, int year, int day, Guid homeId, Guid awayId)
        {
            using (var context = new JodyContext())
            {
                var homeTeam = teamRepository.GetByIdentifier(homeId, context).FirstOrDefault();
                var awayTeam = teamRepository.GetByIdentifier(awayId, context).FirstOrDefault();

                if (homeTeam == null || awayTeam == null)
                {
                    throw new ApplicationException(string.Format("Home or Away Team Does not exist. Home identifier is {0}.  Away Identifier is {1}", homeId, awayId));
                }

                var standings = standingsRepository.GetByIdentifier(standingsIdentifier, context).FirstOrDefault();

                var game = new CompetitionGame(Guid.NewGuid(), standings, day, year, homeTeam, awayTeam, 0, 0, false, false, true);

                gameRepository.Create(game, context);

                context.SaveChanges();

                return GameMapper.GameToGameSummaryViewModel(game);
            }
        }
    }
}
