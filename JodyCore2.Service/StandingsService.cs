using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class StandingsService : IStandingsService
    {
        private readonly IStandingsRepository standingsRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IGameRepository gameRepository;

        public StandingsService(IStandingsRepository _standingsRepository, ITeamRepository _teamRepository, IGameRepository _gameRepository)
        {
            standingsRepository = _standingsRepository;
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
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
                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }
        }

        public IStandingsViewModel Sort(Guid guid)
        {
            using (var context = new JodyContext())
            {
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault();
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

        //TODO Need a process method
        public IStandingsViewModel ProcessGames(Guid standingsIdentifier, IList<Guid> gamesToProcess)
        {
            using (var context = new JodyContext())
            {
                var gameDtos = gameRepository.GetAll(context).Where(g => gamesToProcess.Contains(g.Identifier) && !g.Processed);
                var standings = standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(standingsIdentifier, context)).FirstOrDefault();
                
                

                gameDtos.ToList().ForEach(g =>
                {
                    standings.DefaultProcessGame(g);
                });

                context.SaveChanges();

                return StandingsMapper.StandingsToStandingsViewModel(standings);
            }
        }
    }
}
