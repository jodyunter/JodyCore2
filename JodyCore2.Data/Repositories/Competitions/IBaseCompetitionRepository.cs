using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories.Competitions
{
    public interface IBaseCompetitionRepository<T>:IBaseRepository<T> where T: class, IBO, ICompetition
    {
        IQueryable<T> GetCompetitionsByList(IList<Guid> identifiers, JodyContext context);
    }
}
