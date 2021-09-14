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

  
    }
}
