using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class StandingsRecordViewModel : IStandingsRecordViewModel
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
        public int GamesPlayed { get; set; }
        public int GoalDifference { get; set; }

        public StandingsRecordViewModel(Guid identifier, Guid standingsIdentifier, string standingsName, Guid teamIdentifier, string teamName, int rank, string division, string name, int wins, int regulationWins, int overTimeWins, int shootOutWins, int loses, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst, int points, int gamesPlayed, int goalDifference)
        {
            Identifier = identifier;
            StandingsIdentifier = standingsIdentifier;
            StandingsName = standingsName;
            TeamIdentifier = teamIdentifier;
            TeamName = teamName;
            Rank = rank;
            Division = division;
            Name = name;
            Wins = wins;
            RegulationWins = regulationWins;
            OverTimeWins = overTimeWins;
            ShootOutWins = shootOutWins;
            Loses = loses;
            RegulationLoses = regulationLoses;
            OverTimeLoses = overTimeLoses;
            ShootoutLoses = shootoutLoses;
            Ties = ties;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            Points = points;
            GamesPlayed = gamesPlayed;
            GoalDifference = goalDifference;
        }
    }
}
