using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public class BestOfPlayoffSeries : PlayoffSeries, IBestOfPlayoffSeries
    {
        public int RequiredWins { get; set; }

        public BestOfPlayoffSeries() : base(SeriesType.BestOf) { }

        public override IList<ICompetitionGame> CreateGames()
        {
            if (!Complete)
            {
                var unprocessedGames = Games.Where(g => g.Complete && !g.Processed).ToList();
                if (unprocessedGames.Count > 0)
                {
                    throw new ApplicationException("Can't create new games for series, if some games are unprocessed.");
                }

                var currentInCompleteGames = Games.Where(g => !g.Complete).ToList().Count;

                var team1RequiredWins = RequiredWins - Team1Score;
                var team2RequiredWins = RequiredWins - Team2Score;

                var minimumWinsToFinish = team1RequiredWins <= team2RequiredWins ? team1RequiredWins : team2RequiredWins;

                var neededGames = minimumWinsToFinish - currentInCompleteGames;

                for (int i = 0; i < neededGames; i++)
                {
                    //create games here
                }
            }
            else
            {
                throw new ApplicationException("Trying to create games for a complete series.");
            }
            return null;
        }

        public override ITeam GetLoser()
        {
            if (Complete)
            {
                return Team1Score == RequiredWins ? Team2 : Team1;
            }
            else
            {
                return null;
            }
        }

        public override ITeam GetWinner()
        {
            if (Complete)
            {
                return Team1Score == RequiredWins ? Team1 : Team2;
            }
            else
            {
                return null;
            }
        }

        public override bool IsComplete()
        {
            if (Team1Score == RequiredWins || Team2Score == RequiredWins)
            {
                Complete = true;                
            }
            else
            {
                Complete = false;
            }

            return Complete;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            if (game.Complete && !game.Processed)
            {
                if (game is IPlayoffGame)
                {
                    if (game.GetWinner().Identifier == Team1.Identifier)
                    {
                        Team1Score++;
                    }
                    else if (game.GetWinner().Identifier == Team2.Identifier)
                    {
                        Team2Score++;
                    }
                    else
                    {
                        throw new ApplicationException("Game is not for this playoff series.");
                    }
                }
                else
                {
                    throw new ApplicationException("This is not a playoff game!");
                }
            }
            else
            {
                throw new ApplicationException("Can't process an incomplete game or already processed game.");
            }
        }
    }
}
