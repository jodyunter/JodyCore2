using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class StandingsRecord:IStandingsRecord
    {
        public ITeam Team { get; set; }
        public int Rank { get; set; }
        public string Division { get; set; }
        public string Name { get; set; }
        public int Wins { get { return RegulationWins + OverTimeWins + ShootOutWins; } }
        public int RegulationWins { get; set; }
        public int OverTimeWins { get; set; }
        public int ShootOutWins { get; set; }
        public int Loses { get { return RegulationLoses + OverTimeLoses + ShootoutLoses; } }
        public int RegulationLoses { get; set; }
        public int OverTimeLoses { get; set; }
        public int ShootoutLoses { get; set; }
        public int Ties { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get { return calculatePoints(this); } }
        private Func<IStandingsRecord, int> calculatePoints;

        public int GoalDifference { get { return GoalsFor - GoalsAgainst; } }
        public int GamesPlayed { get { return Wins + Loses + Ties; } }

        public StandingsRecord(int rank, string division, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst, Func<IStandingsRecord, int> points)
        {
            Rank = rank;
            Division = division;
            Name = name;
            RegulationWins = regulationWins;
            OverTimeWins = overTimeWins;
            ShootOutWins = shootOutWins;
            RegulationLoses = regulationLoses;
            OverTimeLoses = overTimeLoses;
            ShootoutLoses = shootoutLoses;
            Ties = ties;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            calculatePoints = points;
        }

        public StandingsRecord(int rank, string division, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst)
        {
            Rank = rank;
            Division = division;
            Name = name;
            RegulationWins = regulationWins;
            OverTimeWins = overTimeWins;
            ShootOutWins = shootOutWins;
            RegulationLoses = regulationLoses;
            OverTimeLoses = overTimeLoses;
            ShootoutLoses = shootoutLoses;
            Ties = ties;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            calculatePoints = this.DefaultGetPoints;
        }
    }
}
