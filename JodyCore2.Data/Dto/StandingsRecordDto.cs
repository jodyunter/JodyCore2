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
    }
}
