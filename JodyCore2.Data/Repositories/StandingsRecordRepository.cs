using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class StandingsRecordRepository:BaseRepository<StandingsRecordDto>, IStandingsRecordRepository
    {
        public override IQueryable<StandingsRecordDto> WithAllObjects(IQueryable<StandingsRecordDto> query)
        {
            return query.Include(s => s.TeamDto)
                        .Include(s => s.StandingsDto);
        }
    }
}
