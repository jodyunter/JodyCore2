using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain;
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

        public StandingsService(IStandingsRepository _standingsRepository, ITeamRepository _teamRepository)
        {
            standingsRepository = _standingsRepository;
            teamRepository = _teamRepository;
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

        public IStandingsViewModel GetByIdentifier(Guid guid)
        {
            using (var context = new JodyContext())
            {
                return StandingsMapper.StandingsToStandingsViewModel(standingsRepository.WithAllObjects(standingsRepository.GetByIdentifier(guid, context)).FirstOrDefault());
            }            
        }
    }
}
