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
        public static string NO_TEAM_HAS_REQUIRED_SCORE = "Neither team has the required score.";
        public static string GAME_INCOMPLETE_OR_PROCESSED =  "Can't process an incomplete game or already processed game.";
        public static string WRONG_SERIES_FOR_GAME = "Game is not for this playoff series.";
        public static string WRONG_TEAMS_FOR_SERIES = "Teams in game are not in this series.";
        public static string NOT_PLAYOFF_GAME = "This is not a playoff game!";
        public static string SERIES_COMPLETE_CANT_PROCESS_GAMES = "Can't process a game when series is complete.";
        public static string UNPROCESSED_GAMES_EXIST = "Can't create new games for series, if some games are unprocessed.";
        public static string SERIES_COMPLETE_CANT_CREATE_GAMES = "Trying to create games for a complete series.";

        public int RequiredWins { get; set; }

        public BestOfPlayoffSeries() : base(SeriesType.BestOf) { }

        public BestOfPlayoffSeries(Guid identifier, IPlayoff playoff, string name, int round, int requiredWins, ITeam team1, ITeam team2,
            ICompetitionRankingGroup team1FromGroup, int team1FromRank,
            ICompetitionRankingGroup team2FromGroup, int team2FromRank,
            ICompetitionRankingGroup winnerGoesTo, ICompetitionRankingGroup winnerRankFrom,
            ICompetitionRankingGroup loserGoesTo, ICompetitionRankingGroup loserRankFrom,            
            int team1Score, int team2Score, string homeString,
            bool processed, bool complete):base(identifier, playoff, name, round, team1, team2, 
                team1FromGroup, team1FromRank, team2FromGroup, team2FromRank,
                winnerGoesTo,winnerRankFrom, loserGoesTo, loserRankFrom, 
                team1Score, team2Score, homeString, processed, complete, SeriesType.BestOf)
            
        {
            RequiredWins = requiredWins;
        }

        public override IList<ICompetitionGame> CreateGames(IList<ICompetitionGame> seriesGames)
        {
            var newGames = new List<ICompetitionGame>();

            if (!Complete)
            {
                var unprocessedGames = seriesGames.Where(g => g.Complete && !g.Processed).ToList();
                if (unprocessedGames.Count > 0)
                {
                    throw new ApplicationException(UNPROCESSED_GAMES_EXIST);
                }

                var currentInCompleteGames = seriesGames.Where(g => !g.Complete).ToList().Count;

                var team1RequiredWins = RequiredWins - Team1Score;
                var team2RequiredWins = RequiredWins - Team2Score;

                var minimumWinsToFinish = team1RequiredWins <= team2RequiredWins ? team1RequiredWins : team2RequiredWins;

                var neededGames = minimumWinsToFinish - currentInCompleteGames;

                for (int i = 0; i < neededGames; i++)
                {
                    var newGame = CreateGame(seriesGames);
                    newGames.Add(newGame);
                }
            }
            else
            {
                //do nothing don't throw an error
            }
            return newGames;
        }

        public override ITeam GetLoser()
        {
            if (Complete)
            {
                if (Team1Score == RequiredWins)
                {
                    return Team2;
                }
                else if (Team2Score == RequiredWins)
                {
                    return Team1;
                }
                else
                {
                    throw new ApplicationException(NO_TEAM_HAS_REQUIRED_SCORE);
                }
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
                if (Team1Score == RequiredWins)
                {
                    return Team1;
                }
                else if (Team2Score == RequiredWins)
                {
                    return Team2;
                }
                else
                {
                    throw new ApplicationException(NO_TEAM_HAS_REQUIRED_SCORE);
                }
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
            if (!Complete)
            {
                if (game.Complete && !game.Processed)
                {
                    if (game is IPlayoffGame)
                    {
                        var playoffGame = (IPlayoffGame)game;

                        if (playoffGame.Series.Identifier.Equals(this.Identifier))
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
                                throw new ApplicationException(WRONG_TEAMS_FOR_SERIES);
                            }

                            game.Process();
                            this.IsComplete(); //be sure to check and set if complete
                        }
                        else
                        {
                            throw new ApplicationException(WRONG_SERIES_FOR_GAME);
                        }
                    }
                    else
                    {
                        throw new ApplicationException(NOT_PLAYOFF_GAME);
                    }
                }
                else
                {
                    throw new ApplicationException(GAME_INCOMPLETE_OR_PROCESSED);
                }
            }
            else
            {
                throw new ApplicationException(SERIES_COMPLETE_CANT_PROCESS_GAMES);
            }
        }
    }
}
