using JodyCore2.Data.Repositories.Games;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories.Competitions
{
    public interface ICompetitionGameRepository : IBaseGameRepository<CompetitionGame>
    {
        IQueryable<CompetitionGame> GetByCompetition(Guid competitionId, JodyContext context);
    }
}
