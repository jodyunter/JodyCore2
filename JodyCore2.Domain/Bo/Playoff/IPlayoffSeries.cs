using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{

    public interface IPlayoffSeries: IBO
    {
        IPlayoff Playoff { get;  }
        string Name { get; }
        int Round { get;  }
        ITeam Team1 { get; }
        ITeam Team2 { get; }
        ICompetitionRankingGroup Team1FromGroup { get; }
        int Team1FromRank { get; }
        ICompetitionRankingGroup Team2FromGroup { get; }
        int Team2FromRank { get; }
        ICompetitionRankingGroup WinnerGoesTo { get; }
        ICompetitionRankingGroup WinnerRankFrom { get; }
        ICompetitionRankingGroup LoserGoesTo { get; }
        ICompetitionRankingGroup LoserRankFrom { get; }        
        int Team1Score { get; } //for some playoff types this is games won, others could be total goals
        int Team2Score { get; }
        ITeam GetWinner();
        ITeam GetLoser();
        bool Complete { get; }
        bool Processed { get; }
        SeriesType SeriesType { get; }
        void ProcessGame(ICompetitionGame game);
        bool IsComplete();
        IList<ICompetitionGame> CreateGames(IList<ICompetitionGame> seriesGames);
        string HomeString { get; } //this says which team will be home/away
        ICompetitionGame CreateGame(IList<ICompetitionGame> seriesGames);
        void SetTeam1(ITeam team);
        void SetTeam2(ITeam team);
        void Process();        
        
    }

    public enum SeriesType
    {
        BestOf = 1,
        TotalGoals = 2
    }
}
