using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Rankings
{
    public interface IRankingGroup
    {
        Guid Identifier { get; }
        string Name { get; }
        IList<IRanking> Rankings { get; }

        IRanking GetRankingByTeam(ITeam team);
        void SetRank(ITeam team, int rank);
    }
}
