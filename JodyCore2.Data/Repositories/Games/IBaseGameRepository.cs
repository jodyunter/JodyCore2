using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using System.Linq;

namespace JodyCore2.Data.Repositories.Games
{
    public interface IBaseGameRepository<T> : IBaseRepository<T> where T : class, IGame, IBO
    {
        IQueryable<T> GetByYearAndDayRange(int year, int firstDay, int? lastDay, JodyContext context);
        IQueryable<T> GetByYearAndDayRangeAndCompleteStatus(int year, int firstDay, int? lastDay, bool complete, JodyContext context);
    }
}
