using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class StandingsService : IStandingsService
    {
        private readonly IStandingsRepository standingsRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IGameRepository gameRepository;
        private readonly IRankingGroupRepository rankingGroupRepository;

        public StandingsService(IStandingsRepository _standingsRepository, ITeamRepository _teamRepository, IGameRepository _gameRepository, IRankingGroupRepository _rankingGroupRepository)
        {
            standingsRepository = _standingsRepository;
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
            rankingGroupRepository = _rankingGroupRepository;
        }

        public IStandingsViewModel Create(string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<ITeamViewModel> teamsToInclude)
        {
            //todo CHECSK ON NAME ETC
            using (var context = new JodyContext()) 
            {
                var standings = new StandingsDto(Guid.NewGuid(), name, startYear, endYear, startDay, endDay, description, division, null);

                var teamDtoList = teamsToInclude.Select(t => teamRepository.GetByIdentifier(t.Identifier, context).FirstOrDefault()).ToList();
                var standingsRecordList = teamDtoList.Select(ta => new StandingsRecordDto(Guid.NewGuid(), standings, ta, 1, division, ta.Name, 0, 0, 0, 0, 0, 0, 0, 0, 0)).ToList();

                standings.Records = standingsRecordList.ToList<IStandingsRecord>();

                context.Add(standings);
                //create the default rankings
                var rankings = new RankingGroupDto(Guid.NewGuid(), "Default", new List<RankingDto>());
                teamDtoList.ForEach(team =>
                {
                    rankings.RankingsDto.Add(new RankingDto(Guid.NewGuid(), team, 1, rankings));
                });

                context.Add(rankings);

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }
        }

        public IStandingsViewModel Sort(Guid guid)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault();
                //todo add a get by standings method
                var rankingGroup = rankingGroupRepository.GetAll(context).Where(rg => rg.Standings.Identifier == standings.Identifier).FirstOrDefault();

                var sortedRecords = standings.Records.ToList().OrderByDescending(r => r.Points)
                            .ThenBy(r => r.GamesPlayed)
                            .ThenByDescending(r => r.Wins)
                            .ThenByDescending(r => r.GoalDifference)
                            .ThenByDescending(r => r.GoalsFor).ToList();

                for (int i = 0; i < sortedRecords.Count(); i++)
                {
                    sortedRecords[i].Rank = i + 1;
                }

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }
        }

        public IStandingsViewModel GetByIdentifier(Guid guid)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault();
                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }            
        }

        //Process Games will process all incomplete and unprocessed games
        public IStandingsViewModel ProcessGames(Guid standingsIdentifier)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(standingsIdentifier, context)).FirstOrDefault();
                var gameDtos = gameRepository.GetAll(context).Where(g => !g.Processed && g.Complete && g.StandingsDto.Identifier == standingsIdentifier);
                
                               
                gameDtos.ToList().ForEach(g =>
                {
                    standings.DefaultProcessGame(g);
                });

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }
        }
        public IList<IGameSummaryViewModel> GetStandingsGames(Guid standingsIdentifier, int year, int firstDay, int lastDay)
        {
            using (var context = new JodyContext())
            {
                return gameRepository.GetByYearAndDayRange(year, firstDay, lastDay, context).Where(g => g.Identifier == standingsIdentifier).Select(g => GameMapper.GameToGameSummaryViewModel(g)).ToList();
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

                var game = new GameDto(Guid.NewGuid(), standings, year, day, homeTeam, awayTeam, 0, 0, false, false, true);

                gameRepository.Create(game, context);

                context.SaveChanges();

                return GameMapper.GameToGameSummaryViewModel(game);
            }
        }
    }
}
