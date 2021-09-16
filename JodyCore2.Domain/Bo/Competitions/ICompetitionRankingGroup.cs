using JodyCore2.Domain.Bo.Rankings;
using System.Collections.Generic;

namespace JodyCore2.Domain.Bo.Competitions
{
    public interface ICompetitionRankingGroup : IRankingGroup
    {
        IList<ICompetition> Competitions { get; }
    }
}
