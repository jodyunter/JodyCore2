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
    public static class CompetitionMapper
    {
        public static ISimpleCompetitionViewModel CompetitionToSimpleCompetitionViewModel(ICompetition competition)
        {
            return new SimpleCompetitionViewModel(competition.Identifier, competition.Name);
        }
        public static IStandingsViewModel StandingsToStandingsViewModel(IStandings standings, ICompetitionRankingGroup rankingGroup)
        {
            var records = standings.Records.Where(r => rankingGroup.GetRankingByTeam(r.Team) != null).Select(s =>
                StandingsRecordMapper.StandingsRecordToStandingsRecordViewModel(s,
                rankingGroup.GetRankingByTeam(s.Team).Rank,
                rankingGroup.Name)

            ).ToList();
            return new StandingsViewModel(standings.Identifier,
                standings.Name,
                standings.StartYear,
                standings.StartDay,
                standings.Description,
                standings.Division,
                records);
        }
    }
}
