using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public abstract class PlayoffSeries : IPlayoffSeries, IBO
    {
        public Guid Identifier { get; set; }   
        
        public IPlayoff Playoff { get; set; }
        public ITeam Team1 { get; set; }
        public ITeam Team2 { get; set; }

        public ICompetitionRankingGroup Team1FromGroup { get; set; }

        public int Team1FromRank { get; set; }

        public ICompetitionRankingGroup Team2FromGroup { get; set; }

        public int Team2FromRank { get; set; }

        public ICompetitionRankingGroup WinnerGoesTo { get; set; }

        public ICompetitionRankingGroup WinnerRankFrom { get; set; }

        public ICompetitionRankingGroup LoserGoesTo { get; set; }

        public ICompetitionRankingGroup LoserRankFrom { get; set; }

        public IList<IPlayoffGame> Games { get; set; }

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        public bool Complete { get; set; }

        public SeriesType SeriesType { get; set; }

        public string HomeString { get; set; }

        public abstract IList<ICompetitionGame> CreateGames();

        public abstract ITeam GetLoser();

        public abstract ITeam GetWinner();

        public abstract bool IsComplete();

        public abstract void ProcessGame(ICompetitionGame game);

        public ITeam GetHomeTeamForGame(int gameNumber)
        {
            int arrayNumber = gameNumber - 1;

            if (HomeString.Length < arrayNumber)
            {
                if (arrayNumber % 2 == 0)
                {
                    return Team1;
                }
                else
                {
                    return Team2;
                }
            }
            else
            {
                var value = HomeString[arrayNumber];

                if (value != '1' || value != '2')
                {
                    throw new ArgumentOutOfRangeException(string.Format("{0} contains something other than 1 or 2", HomeString));
                }
                else
                {
                    if (value.Equals('1'))
                    {
                        return Team1;
                    }
                    else
                    {
                        return Team2;
                    }                    
                }
            }
        }
        public ICompetitionGame CreateGame()
        {
            int gameNumber = Games.Count + 1;

            var homeTeam = GetHomeTeamForGame(gameNumber);
            ITeam awayTeam = null;

            if (homeTeam.Identifier == Team1.Identifier)
            {
                awayTeam = Team2;
            }
            else
            {
                awayTeam = Team1;
            }

            //day and year has to be done at scheduling time
            //need to have game rules!
            var game = new CompetitionGame(Guid.NewGuid(), Playoff, -1, -1, homeTeam, awayTeam, 0, 0, false, false, false);

            return game;
        }
    }
}
