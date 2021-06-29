using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
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

            return AlwaysInclude(query);
        }

        public IQueryable<GameDto> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, bool complete, JodyContext context)
        {
            var query = GetByYearAndDayRange(year, firstDay, lastDay, context).Where(g => g.Complete == complete);

            return AlwaysInclude(query);
        }

        public override IQueryable<GameDto> AlwaysInclude(IQueryable<GameDto> query)
        {
            return query.Include(g => g.HomeDto)
                        .Include(g => g.AwayDto);
        }

    }
}
