namespace JodyCore2.Domain.Bo.Competitions
{
    public interface ICompetitionGame : IGame
    {
        ICompetition Competition { get; }
    }
}
