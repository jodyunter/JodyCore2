using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Domain.Bo.Standings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JodyCore2.Data.Repositories.Standing
{
    public class StandingsRepository : BaseCompetitionRepository<Standings>, IStandingsRepository
    {
        public override IQueryable<Standings> WithAllObjects(IQueryable<Standings> query)
        {
            return query.Include(s => s.Records).ThenInclude(r => r.Team);
        }
    }
}
