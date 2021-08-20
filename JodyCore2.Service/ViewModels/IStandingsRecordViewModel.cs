using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IStandingsRecordViewModel
    {
        public Guid Identifier { get; }
        public Guid StandingsIdentifier { get; }
        public string StandingsName { get; }
        public Guid TeamIdentifier { get; }
        public string TeamName { get; }
        public int Rank { get; }
        public string Division { get; }
        public string Name { get; }
        public int Wins { get; }
        public int RegulationWins { get; }
        public int OverTimeWins { get; }
        public int ShootOutWins { get; }
        public int Loses { get; }
        public int RegulationLoses { get; }
        public int OverTimeLoses { get; }
        public int ShootoutLoses { get; }
        public int Ties { get; }
        public int GoalsFor { get; }
        public int GoalsAgainst { get; }
        public int Points { get; }
        public int GamesPlayed { get; }
        public int GoalDifference { get; }
    }
}
