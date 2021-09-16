using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class StandingsRecord:IStandingsRecord, IBO
    {
        public Guid Identifier { get; set; }
        public virtual IStandings ParentStandings { get; set; }
        public virtual ITeam Team { get; set; }
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
        protected Func<IStandingsRecord, int> calculatePoints;
        public int GoalDifference { get { return GoalsFor - GoalsAgainst; } }
        public int GamesPlayed { get { return Wins + Loses + Ties; } }

        public StandingsRecord() 
        {
            calculatePoints = this.DefaultGetPoints;
        }

        //quick constructor, new records
        public StandingsRecord(IStandings standings, ITeam team, string name)
            :this(Guid.NewGuid(), standings, team, name, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        //full constructor no guid
        public StandingsRecord(IStandings standings, ITeam team, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst, Func<IStandingsRecord, int> points)
            :this(Guid.NewGuid(), standings, team, name, regulationWins, overTimeWins, shootOutWins, regulationLoses, overTimeLoses, shootoutLoses, ties, goalsFor, goalsAgainst, points)
        {

        }

        //full constructor
        public StandingsRecord(Guid identifier, IStandings standings, ITeam team, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst, Func<IStandingsRecord, int> points)
        {
            Identifier = identifier;
            ParentStandings = standings;
            Team = team;
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

        //full constructor default points
        public StandingsRecord(Guid identifier, IStandings standings, ITeam team, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst)
        {
            Identifier = identifier;
            ParentStandings = standings;
            Team = team;
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

        public override bool Equals(object obj)
        {
            return obj is StandingsRecord record &&
                   Identifier.Equals(record.Identifier) &&
                   ParentStandings.Identifier.Equals(record.ParentStandings.Identifier) &&
                   Team.Identifier.Equals(record.Team.Identifier) &&
                   Name == record.Name &&
                   Wins == record.Wins &&
                   RegulationWins == record.RegulationWins &&
                   OverTimeWins == record.OverTimeWins &&
                   ShootOutWins == record.ShootOutWins &&
                   Loses == record.Loses &&
                   RegulationLoses == record.RegulationLoses &&
                   OverTimeLoses == record.OverTimeLoses &&
                   ShootoutLoses == record.ShootoutLoses &&
                   Ties == record.Ties &&
                   GoalsFor == record.GoalsFor &&
                   GoalsAgainst == record.GoalsAgainst;                                     
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Identifier);
            hash.Add(ParentStandings);
            hash.Add(Team);
            hash.Add(Name);
            hash.Add(Wins);
            hash.Add(RegulationWins);
            hash.Add(OverTimeWins);
            hash.Add(ShootOutWins);
            hash.Add(Loses);
            hash.Add(RegulationLoses);
            hash.Add(OverTimeLoses);
            hash.Add(ShootoutLoses);
            hash.Add(Ties);
            hash.Add(GoalsFor);
            hash.Add(GoalsAgainst);
            hash.Add(Points);
            hash.Add(calculatePoints);
            hash.Add(GoalDifference);
            hash.Add(GamesPlayed);
            return hash.ToHashCode();
        }
    }
}
