using JodyCore2.Domain.Bo.Standings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JodyCore2.Data.Repositories
{
    public class StandingsRepository:BaseRepository<Standings>, IStandingsRepository
    {
        public override IQueryable<Standings> WithAllObjects(IQueryable<Standings> query)
        {
            return query.Include(s => s.Records).ThenInclude(r => r.Team);                       
        }
    }
}
