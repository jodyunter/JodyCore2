using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Mappers
{
    public static class StandingsMapper
    {
        public static IStandingsViewModel StandingsToStandingsViewModel(IStandings standings, ICompetitionRankingGroup rankingGroup)
        {
            var records = standings.Records.Select(s =>
                StandingsRecordMapper.StandingsRecordToStandingsRecordViewModel(s,
                rankingGroup.GetRankingByTeam(s.Team).Rank,
                rankingGroup.Name)

            ).ToList();
            return new StandingsViewModel(standings.Identifier,
                standings.Name,
                standings.StartYear,
                standings.EndYear,
                standings.StartDay,
                standings.EndDay,
                standings.Description,
                standings.Division,
                records);
        }

    }
}
