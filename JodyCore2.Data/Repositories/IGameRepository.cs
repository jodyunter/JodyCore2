using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public interface IGameRepository:IBaseRepository<GameDto>
    {        
        IQueryable<GameDto> GetByYearAndDayRange(int year, int firstDay, int? lastDay, JodyContext context);
        IQueryable<GameDto> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, bool complete, JodyContext context);        
    }
}
