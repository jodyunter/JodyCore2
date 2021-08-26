using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public interface IStandingsRankingGroup:IRankingGroup
    {
        IStandings Standings { get; }
    }
}
