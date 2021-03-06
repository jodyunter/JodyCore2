using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competitions
{
    //todo:  Generalize the playoff and standings as competitions
    //todo:  Rankings could be moved higher up as a competition ranking.
    public interface ICompetition:IBO
    {        
        string Name { get; }
        int StartYear { get; }        
        int StartDay { get; }
        int Order { get; }
        string Description { get; }
        bool Setup { get; }
        bool Started { get; }
        bool Complete { get; }
        bool Processed { get; }
        CompetitionType CompetitionType { get; }
        IList<ICompetitionRankingGroup> RankingGroups { get; }
        void ProcessGame(ICompetitionGame game);
        void StartCompetition();
        void SetupCompetition();
        void CompleteCompetition();
        void ProcessCompetition();
        ICompetitionGame CreateGame(int year, int day, ITeam home, ITeam away);
    }
    public enum CompetitionType
    {
        Standings = 0,
        Playoff = 1

    }
}
