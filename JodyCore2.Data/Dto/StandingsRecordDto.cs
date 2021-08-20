using JodyCore2.Domain;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class StandingsRecordDto : StandingsRecord, IStandingsRecord, IBaseDto
    {
        public int Id { get; set; }

        public StandingsDto StandingsDto { get; set; }
        public TeamDto TeamDto { get; set; }

        [NotMapped]
        public override IStandings ParentStandings { get { return StandingsDto; } set { StandingsDto = (StandingsDto)value; }  }
        [NotMapped]
        public override ITeam Team { get { return TeamDto; } set { TeamDto = (TeamDto)Team; } }

        public StandingsRecordDto() { }

        public StandingsRecordDto(Guid identifier, StandingsDto standings, TeamDto team, int rank, string division, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst, Func<IStandingsRecord, int> points)
        {
            Identifier = identifier;
            ParentStandings = standings;
            Team = team;
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

        public StandingsRecordDto(Guid identifier, StandingsDto standings, TeamDto team, int rank, string division, string name, int regulationWins, int overTimeWins, int shootOutWins, int regulationLoses, int overTimeLoses, int shootoutLoses, int ties, int goalsFor, int goalsAgainst)
        {
            Identifier = identifier;
            ParentStandings = standings;
            Team = team;
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

        public override bool Equals(object obj)
        {
            return obj is StandingsRecordDto dto &&
                   Identifier.Equals(dto.Identifier) &&
                   ((ParentStandings == null && dto.ParentStandings == null) || EqualityComparer<IStandings>.Default.Equals(ParentStandings, dto.ParentStandings)) &&
                   EqualityComparer<ITeam>.Default.Equals(Team, dto.Team) &&
                   Rank == dto.Rank &&
                   Division == dto.Division &&
                   Name == dto.Name &&                   
                   RegulationWins == dto.RegulationWins &&
                   OverTimeWins == dto.OverTimeWins &&
                   ShootOutWins == dto.ShootOutWins &&                   
                   RegulationLoses == dto.RegulationLoses &&
                   OverTimeLoses == dto.OverTimeLoses &&
                   ShootoutLoses == dto.ShootoutLoses &&
                   Ties == dto.Ties &&
                   GoalsFor == dto.GoalsFor &&
                   GoalsAgainst == dto.GoalsAgainst;                                                                                               
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Identifier);
            hash.Add(ParentStandings);
            hash.Add(Team);
            hash.Add(Rank);
            hash.Add(Division);
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
            hash.Add(Id);
            hash.Add(StandingsDto);
            hash.Add(TeamDto);
            hash.Add(ParentStandings);
            hash.Add(Team);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            var format = "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14}";

            return string.Format(format,
                   Identifier,
                   ParentStandings,
                   Team,
                   Rank,
                   Division,
                   Name,
                   RegulationWins,
                   OverTimeWins,
                   ShootOutWins,
                   RegulationLoses,
                   OverTimeLoses,
                   ShootoutLoses,
                   Ties,
                   GoalsFor,
                   GoalsAgainst);
        }
    }
}
