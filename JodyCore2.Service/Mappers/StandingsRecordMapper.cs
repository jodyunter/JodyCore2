using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Mappers
{
    public static class StandingsRecordMapper
    {
        public static IStandingsRecordViewModel StandingsRecordToStandingsRecordViewModel(IStandingsRecord record)
        {
            return new StandingsRecordViewModel(record.Identifier,
                record.ParentStandings.Identifier,
                record.ParentStandings.Name,
                record.Team.Identifier,
                record.Team.Name,
                record.Rank,
                record.Division,
                record.Name,
                record.Wins,
                record.RegulationWins,
                record.OverTimeWins,
                record.ShootOutWins,
                record.Loses,
                record.RegulationLoses,
                record.OverTimeLoses,
                record.ShootoutLoses,
                record.Ties,
                record.GoalsFor,
                record.GoalsAgainst,
                record.Points,
                record.GamesPlayed,
                record.GoalDifference);
        }
    }
}
