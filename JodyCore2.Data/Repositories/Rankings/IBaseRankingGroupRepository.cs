using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories.Rankings
{
    public interface IBaseRankingGroupRepository<T> : IBaseRepository<T> where T : class, IRankingGroup, IBO
    {
    }
}
