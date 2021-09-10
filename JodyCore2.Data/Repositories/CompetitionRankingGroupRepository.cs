using JodyCore2.Domain.Bo.Competition;
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
            return context.CompetitionRankingGroups.Where(t => t.Identifier == competitionIdentifier);
        }
    }
}
