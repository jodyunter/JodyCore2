using JodyCore2.Domain.Bo.Scheduling;

namespace JodyCore2.Domain.Bo.Competitions
{
    public interface ICompetitionGame : IGame, IScheduleGame
    {
        ICompetition Competition { get; }
    }
}
