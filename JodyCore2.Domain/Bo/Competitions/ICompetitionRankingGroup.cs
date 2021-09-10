using JodyCore2.Domain.Bo.Rankings;

namespace JodyCore2.Domain.Bo.Competitions
{
    public interface ICompetitionRankingGroup : IRankingGroup
    {
        ICompetition Competition { get; }
    }
}
