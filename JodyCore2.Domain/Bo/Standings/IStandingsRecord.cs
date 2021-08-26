using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public interface IStandingsRecord
    {
        Guid Identifier { get; set; }
        IStandings ParentStandings { get; set; }
        ITeam Team { get; set; }
        string Name { get; set; }
        int Wins { get; }
        int RegulationWins { get; set; }
        int OverTimeWins { get; set; }
        int ShootOutWins { get; set; }
        int Loses { get; }
        int RegulationLoses { get; set; }
        int OverTimeLoses { get; set; }
        int ShootoutLoses { get; set; }
        int Ties { get; set; }
        int GoalsFor { get; set; }
        int GoalsAgainst { get; set; }
        int Points { get; }
        int GamesPlayed { get; }
        int GoalDifference { get; }
    }
}
