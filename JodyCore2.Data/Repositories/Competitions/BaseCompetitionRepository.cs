using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories.Competitions
{
    public class BaseCompetitionRepository<T> : BaseRepository<T>, IBaseCompetitionRepository<T>, IBaseRepository<T> where T : class, IBO, ICompetition
    {
        public IQueryable<T> GetCompetitionsByList(IList<Guid> identifiers, JodyContext context)
        {
            return context.Set<T>().Where(c => identifiers.Contains(c.Identifier));
        }
    }
}
