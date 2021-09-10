using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competition
{
    //todo:  Generalize the playoff and standings as competitions
    //todo:  Rankings could be moved higher up as a competition ranking.
    public interface ICompetition
    {        
        Guid Identifier { get; }
        string Name { get; }
        int StartYear { get; }
        int EndYear { get; }
        int StartDay { get; }
        int EndDay { get; }
        string Description { get; }
        CompetitionType CompetitionType { get; }
        void ProcessGame(ICompetitionGame game);
    }
    public enum CompetitionType
    {
        Standings = 0,
        Playoff = 1

    }
}
