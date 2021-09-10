using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Rankings;
using JodyCore2.Domain.Bo.Competitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class CompetitionRankingGroupRepository : BaseRankingGroupRepository<CompetitionRankingGroup>, ICompetitionRankingGroupRepository
    {
        public IQueryable<CompetitionRankingGroup> GetByCompetition(Guid competitionIdentifier, JodyContext context)
        {
            return AlwaysInclude(context.CompetitionRankingGroups.Where(t => t.Competition.Identifier == competitionIdentifier));
        }

        public override IQueryable<CompetitionRankingGroup> AlwaysInclude(IQueryable<CompetitionRankingGroup> query)
        {
            return query.Include(g => g.Rankings).ThenInclude(r => r.Team);
        }
    }
}
