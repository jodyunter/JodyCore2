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
        public IList<ICompetitionRankingGroup> RankingGroups { get; set; }
        public Playoff() : base(CompetitionType.Playoff)
        {
            Series = new List<IPlayoffSeries>();
            RankingGroups = new List<ICompetitionRankingGroup>();
        }

        public IList<IPlayoffSeries> Series { get; set; }

        public override IList<ICompetitionGame> CreateGames()
        {
            var games = new List<ICompetitionGame>();

            Series.Where(s => s.Round == CurrentRound).ToList().ForEach(s =>
            {
                games.AddRange(s.CreateGames());
            });

            return games;
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
                playoffGame.Series.ProcessGame(game);
            }
        }

        public override void SetupCompetition()
        {
            //apply teams to all series that already are setup
            SetupSeriesTeams();
        }    

        public void SetupSeriesTeams()
        {
            Series.ToList().ForEach(series =>
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

        public ITeam GetTeamFromGroup(ICompetitionRankingGroup group, int rank)
        {
            var ranking = group.GetByRank(rank);
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
            SetupSeriesTeams();
        }

        public bool IsCurrentRoundComplete()
        {
            return IsRoundComplete(CurrentRound);
        }

        public void ProcessEndOfCurrentRound()
        {
            if (IsCurrentRoundComplete())
            {
                SetupSeriesTeams();
                bool inCompleteSeriesExists = false;
                Series.ToList().ForEach(s =>
                {
                    if (!s.Complete) inCompleteSeriesExists = true;
                });
                if (!inCompleteSeriesExists)
                {
                    Complete = true;
                }
                else if (IsRoundReady(CurrentRound + 1))
                {
                    CurrentRound++;
                }
                else
                {
                    throw new ApplicationException("Could not prepare next round, wtf?");
                }
            }
        }
    }
}
