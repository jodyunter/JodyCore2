using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class RankingGroupRepository:BaseRepository<RankingGroupDto>, IRankingGroupRepository
    {
        public override IQueryable<RankingGroupDto> AlwaysInclude(IQueryable<RankingGroupDto> query)
        {
            return query.Include(r => r.RankingsDto).ThenInclude(r => r.TeamDto)
                .Include(r => r.StandingsDto);
        }

        public IQueryable<RankingGroupDto> GetByStandings(Guid standingsIdentifier, JodyContext context)
        {
            return AlwaysInclude(context.RankingGroups.Where(rg => rg.StandingsDto.Identifier == standingsIdentifier));
        }
    }
}
