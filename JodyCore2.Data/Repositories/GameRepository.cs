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
        public IList<GameDto> GetByYearAndDayRange(int year, int firstDay, int? lastDay, JodyContext context)
        {
            throw new NotImplementedException();
        }

        public IList<GameDto> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
