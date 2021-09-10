using JodyCore2.Domain.Bo.Standings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class StandingsRecordRepository:BaseRepository<StandingsRecord>, IStandingsRecordRepository
    {
        public override IQueryable<StandingsRecord> WithAllObjects(IQueryable<StandingsRecord> query)
        {
            return query.Include(s => s.Team)
                        .Include(s => s.ParentStandings);
        }
    }
}
