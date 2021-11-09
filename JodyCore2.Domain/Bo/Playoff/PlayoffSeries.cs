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
        public string Name { get; set; }
        public int Round { get; set; }

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

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        public bool Complete { get; set; }

        public SeriesType SeriesType { get; set; }

        public string HomeString { get; set; }

        public bool Processed { get; set; }

        public abstract IList<IPlayoffGame> CreateGames(IList<IPlayoffGame> games);

        public abstract ITeam GetLoser();

        public abstract ITeam GetWinner();

        public abstract bool IsComplete();

        public abstract void ProcessGame(IPlayoffGame game);

        public virtual void SetTeam1(ITeam team)
        {
            Team1 = team;
        }

        public virtual void SetTeam2(ITeam team)
        {
            Team2 = team;
        }

        public void Process()
        {
            var winner = GetWinner();
            var loser = GetLoser();

            if (WinnerGoesTo != null)
            {
                var winnerRank = WinnerRankFrom.GetRankingByTeam(winner).Rank;
                WinnerGoesTo.SetRank(winner, winnerRank);
            }
            
            if (LoserGoesTo != null)
            {
                var loserRank = LoserRankFrom.GetRankingByTeam(loser).Rank;
                LoserGoesTo.SetRank(loser, loserRank);
            }
            
            Processed = true;
        }

        public PlayoffSeries(SeriesType type):this()
        { 
            SeriesType = type;            
        }
        public PlayoffSeries() { Identifier = Guid.NewGuid(); }

        protected PlayoffSeries(Guid identifier, IPlayoff playoff, string name, int round, ITeam team1, ITeam team2,
            ICompetitionRankingGroup team1FromGroup, int team1FromRank,
            ICompetitionRankingGroup team2FromGroup, int team2FromRank,
            ICompetitionRankingGroup winnerGoesTo, ICompetitionRankingGroup winnerRankFrom, 
            ICompetitionRankingGroup loserGoesTo, ICompetitionRankingGroup loserRankFrom,              
            int team1Score, int team2Score, string homeString, 
            bool processed, bool complete, 
            SeriesType seriesType)
        {
            Identifier = identifier;
            Playoff = playoff;
            Name = name;
            Round = round;
            Team1 = team1;
            Team2 = team2;
            Team1FromGroup = team1FromGroup;
            Team1FromRank = team1FromRank;
            Team2FromGroup = team2FromGroup;
            Team2FromRank = team2FromRank;
            WinnerGoesTo = winnerGoesTo;
            WinnerRankFrom = winnerRankFrom;
            LoserGoesTo = loserGoesTo;
            LoserRankFrom = loserRankFrom;            
            Team1Score = team1Score;
            Team2Score = team2Score;
            HomeString = homeString;
            Processed = processed;
            Complete = complete;
            SeriesType = seriesType;
        }

        public ITeam GetHomeTeamForGame(int gameNumber)
        {
            int arrayNumber = gameNumber - 1;
            if (HomeString == null) HomeString = "";

            if (HomeString.Length < gameNumber)
            {
                if (gameNumber % 2 == 0)
                {
                    return Team2;
                }
                else
                {
                    return Team1;
                }
            }
            else
            {
                var value = HomeString[arrayNumber];

                if (!value.Equals('1') && !value.Equals('2'))
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
        public IPlayoffGame CreateGame(IList<IPlayoffGame> seriesGames)
        {
            int gameNumber = seriesGames.Count();

            if (gameNumber == 0) gameNumber = 1;

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
            var game = new PlayoffGame(Guid.NewGuid(), Playoff, this, -1, -1, homeTeam, awayTeam, 0, 0, false, false, false);            

            return game;
        }
    }
}
