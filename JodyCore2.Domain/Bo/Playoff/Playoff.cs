using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public class Playoff : Competition, IPlayoff
    {
        public int CurrentRound { get; set; }        
        public Playoff() : base(CompetitionType.Playoff)
        {
            Series = new List<IPlayoffSeries>();
            RankingGroups = new List<ICompetitionRankingGroup>();
        }

        public Playoff(string name, int startYear, int startDay, int order, string description, IList<IPlayoffSeries> series, IList<ICompetitionRankingGroup> rankingGroups, bool setup, bool started, bool complete, bool processed)
            :this(Guid.NewGuid(), name, startYear, startDay, order, description, series, rankingGroups, setup, started, complete, processed)
        {            
        }

        public Playoff(Guid identifier, string name, int startYear, int startDay, int order, string description, IList<IPlayoffSeries> series, IList<ICompetitionRankingGroup> rankingGroups, bool setup, bool started, bool complete, bool processed)
            :base(identifier, name, startYear, startDay, order, description, setup, started, complete, processed, rankingGroups, CompetitionType.Playoff)
        {
            Series = series;  
        }

        public IList<IPlayoffSeries> Series { get; set; }

        public override IList<ICompetitionGame> CreateGames(IList<ICompetitionGame> games)
        {
            var newGames = new List<IPlayoffGame>();
            var oldGames = games.ToList().Select(g => (IPlayoffGame)g);

            Series.Where(s => s.Round == CurrentRound).ToList().ForEach(s =>
            {
                
                newGames.AddRange(s.CreateGames(oldGames.Where(g => g.Series.Identifier == s.Identifier).ToList()));
            });

            return newGames.ToList<ICompetitionGame>();
        }

        public IList<IPlayoffSeries> GetByRound(int round)
        {
            return Series.Where(s => s.Round.Equals(round)).ToList();
        }

        public bool IsRoundComplete(int round)
        {
            //a round is complete if we can't find an incomplete series in the round
            return Series.Where(s => s.Round.Equals(round) && !s.Complete).ToList().Count == 0;
        }

        public bool IsRoundReady(int round)
        {
            //a round is ready if every team for each series in the round is populated
            return Series.Where(s => s.Round.Equals(round) && s.Team1 != null && s.Team2 != null).ToList().Count == 0;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            if (game is IPlayoffGame)
            {
                var playoffGame = (IPlayoffGame)game;
                //the game should have it's series already so ti should be able to process it
                playoffGame.Series.ProcessGame(playoffGame);
            }
        }

        public override void SetupCompetition()
        {
            //apply teams to all series that already are setup
            SetupSeriesTeams(CurrentRound);
            base.SetupCompetition();
            
        }    

        public void SetupSeriesTeams(int roundNumber)
        {
            Series.Where(s => s.Round == roundNumber).ToList().ForEach(series =>
            {
                SetupSeriesTeams(series);
            });
        }
        public void SetupSeriesTeams(IPlayoffSeries series)
        {
            if (series.Team1 == null)
            {
                series.SetTeam1(GetTeamFromGroup(series.Team1FromGroup, series.Team1FromRank));                
            }
            if (series.Team2 == null)
            {
                series.SetTeam2(GetTeamFromGroup(series.Team2FromGroup, series.Team2FromRank));
            }
        }

        //instead of getting the specific rank, we should ORDER and pick the team in that order
        public ITeam GetTeamFromGroup(ICompetitionRankingGroup group, int rank)
        {
            var ranking = group.GetByOrder(rank);
            if (ranking!= null)
            {
                return ranking.Team;
            }
            else
            {
                return null;
            }
        }

        public override void StartCompetition()
        {
            CurrentRound = 1;
            SetupSeriesTeams(1);
            base.StartCompetition();
        }

        public bool IsCurrentRoundComplete()
        {
            return IsRoundComplete(CurrentRound);
        }

        public void ProcessEndOfCurrentRound()
        {
            if (IsCurrentRoundComplete())
            {                
                bool inCompleteSeriesExists = false;           
                //check to see if the Playoff is complete and to process complete and unprocessed series.
                Series.ToList().ForEach(s =>
                {
                    if (!s.Complete) inCompleteSeriesExists = true;
                    else
                    {                        
                        if (!s.Processed && s.IsComplete())
                        {
                            s.Process();
                        }
                    }
                });

                if (!inCompleteSeriesExists)
                {
                    Complete = true;
                }
                else
                {
                    CurrentRound += 1;
                    SetupSeriesTeams(CurrentRound);
                }
            }
        }

        public override ICompetitionGame CreateGame(int year, int day, ITeam home, ITeam away)
        {
            //do nothing, should never be called!
            return null;
        }
    }
}
