using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class GameRepository : BaseRepository<GameDto>, IGameRepository
    {
        public IQueryable<GameDto> GetByYearAndDayRange(int year, int firstDay, int? lastDay, JodyContext context)
        {
            var query = context.Games.Where(g => g.Year == year && g.Day >= firstDay);

            if (lastDay != null && lastDay >= firstDay)
            {
                query = query.Where(g => g.Day <= lastDay);
            }

            return query;
        }

        public IQueryable<GameDto> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, bool complete, JodyContext context)
        {
            var query = context.Games.Where(g => g.Year == year && g.Day >= firstDay);

            if (lastDay != null && lastDay >= firstDay)
            {
                query = query.Where(g => g.Day <= lastDay);
            }

            query.Where(g => g.Complete == complete);

            return query;
        }
    }
}
