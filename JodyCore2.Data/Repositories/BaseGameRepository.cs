using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class BaseGameRepository<T>:BaseRepository<T>, IBaseGameRepository<T> where T : class, IGame, IBO
    {
        public IQueryable<T> GetByYearAndDayRange(int year, int firstDay, int? lastDay, JodyContext context)
        {
            var query = context.Games.Where(g => g.Year == year && g.Day >= firstDay);

            if (lastDay != null && lastDay >= firstDay)
            {
                query = query.Where(g => g.Day <= lastDay);
            }

            return AlwaysInclude((IQueryable<T>)query);
        }

        public IQueryable<T> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, bool complete, JodyContext context)
        {
            var query = GetByYearAndDayRange(year, firstDay, lastDay, context).Where(g => g.Complete == complete);

            return AlwaysInclude(query);
        }

        public override IQueryable<T> AlwaysInclude(IQueryable<T> query)
        {
            return query.Include(g => g.Home)
                        .Include(g => g.Away);
        }
    }
}
