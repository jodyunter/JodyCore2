using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{

    public interface IPlayoffSeries
    {
        ICompetitionRankingGroup Team1FromGroup { get; }
        int Team1FromRank { get; }
        ICompetitionRankingGroup Team2FromGroup { get; }
        int Team2FromRank { get; }

        ICompetitionRankingGroup WinnerGoesTo { get; }
        ICompetitionRankingGroup WinnerRankFrom { get; }
        ICompetitionRankingGroup LoserGoesTo { get; }
        ICompetitionRankingGroup LoserRankFrom { get; }

        IList<IPlayoffGame> Games { get; }

        int Team1Score { get; } //for some playoff types this is games won, others could be total goals
        int Team2Score { get; }
        ITeam GetWinner();
        ITeam GetLoser();
        void ProcessGame(ICompetitionGame game);
        bool IsComplete();
        IList<ICompetitionGame> CreateGames();
        
    }
}
