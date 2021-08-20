using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class StandingsRepository:BaseRepository<StandingsDto>, IStandingsRepository
    {
        public override IQueryable<StandingsDto> WithAllObjects(IQueryable<StandingsDto> query)
        {
            return query.Include(s => s.RecordsDto).ThenInclude(r => r.TeamDto);                       
        }
    }
}
