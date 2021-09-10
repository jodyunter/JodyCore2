using JodyCore2.Domain.Bo.Competition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class CompetitionGameRepository : BaseGameRepository<CompetitionGame>, ICompetitionGameRepository
    {
        public IQueryable<CompetitionGame> GetByCompetition(Guid competitionId, JodyContext context)
        {
            return context.CompetitionGames.Where(g => g.Competition.Identifier == competitionId);
        }
    }
}
