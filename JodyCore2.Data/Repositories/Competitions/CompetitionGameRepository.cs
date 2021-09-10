using JodyCore2.Data.Repositories.Games;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Linq;

namespace JodyCore2.Data.Repositories.Competitions
{
    public class CompetitionGameRepository : BaseGameRepository<CompetitionGame>, ICompetitionGameRepository
    {
        public IQueryable<CompetitionGame> GetByCompetition(Guid competitionId, JodyContext context)
        {
            return context.CompetitionGames.Where(g => g.Competition.Identifier == competitionId);
        }
    }
}
