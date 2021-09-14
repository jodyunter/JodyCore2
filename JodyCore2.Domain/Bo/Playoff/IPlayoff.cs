using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public interface IPlayoff:ICompetition
    {
        IList<IPlayoffSeries> Series { get; }                        
        IList<ICompetitionRankingGroup> RankingGroups { get; }
        bool IsRoundComplete(int round);
        bool IsCurrentRoundComplete();
        void ProcessEndOfCurrentRound();
        bool IsRoundReady(int round);                
        int CurrentRound { get; }
        IList<IPlayoffSeries> GetByRound(int round);
    }
}
