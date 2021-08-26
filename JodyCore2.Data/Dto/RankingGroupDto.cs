using JodyCore2.Domain.Bo.Rankings;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class RankingGroupDto : RankingGroup, IRankingGroup, IStandingsRankingGroup, IBaseDto
    {
        public int Id { get; set; }
        public IList<RankingDto> RankingsDto { get; set; }
        public StandingsDto StandingsDto { get; set; }

        [NotMapped]
        public IStandings Standings { get { return StandingsDto; } set { StandingsDto = (StandingsDto)value; } }

        [NotMapped]
        public override IList<IRanking> Rankings { get { return RankingsDto.ToList<IRanking>(); } set { RankingsDto = value.Select(r => (RankingDto)r).ToList(); } }

        public RankingGroupDto() { }
        public RankingGroupDto(Guid identifier, string name, IList<RankingDto> rankings):base()
        {
            Identifier = identifier;
            Name = name;
            RankingsDto = rankings;
        }
    }
}
