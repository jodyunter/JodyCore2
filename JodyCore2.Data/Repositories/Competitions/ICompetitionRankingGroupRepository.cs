using JodyCore2.Data.Repositories.Rankings;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories.Competitions
{
    public interface ICompetitionRankingGroupRepository : IBaseRankingGroupRepository<CompetitionRankingGroup>
    {
        IQueryable<CompetitionRankingGroup> GetByCompetition(Guid competitionIdentifier, JodyContext context);
    }
}
