using JodyCore2.Domain;
using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class RankingDto:Ranking, IRanking, IBaseDto
    {
        public int Id { get; set; }

        public TeamDto TeamDto { get; set; }
        public RankingGroupDto GroupDto { get; set; }
        [NotMapped]
        public override IRankingGroup Group { get { return GroupDto; } set { GroupDto = (RankingGroupDto)value; } }
        [NotMapped]
        public override ITeam Team { get { return TeamDto; } set { TeamDto = (TeamDto)value; } }

        public RankingDto() { }

        public RankingDto(Guid identifier, TeamDto team, int rank, RankingGroupDto group):base()
        {
            Identifier = identifier;
            Team = team;
            Rank = rank;
            Group = group;
        }
    }
}
