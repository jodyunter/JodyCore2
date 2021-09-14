using JodyCore2.ConsoleApp.Views;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp
{
    class TestPlayoffs
    {

        public void run()
        {

            
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);
            var team3 = new Team(Guid.NewGuid(), "Team 3", 5);
            var team4 = new Team(Guid.NewGuid(), "Team 4", 5);

            var playoff = new Playoff()
            {
                CurrentRound = 1,
                Series = new List<IPlayoffSeries>()
            };

            var rankingGroup = new CompetitionRankingGroup(Guid.NewGuid(), playoff, "Main", new List<ICompetitionRanking>());
            
            rankingGroup.Rankings.Add(new CompetitionRanking(Guid.NewGuid(), team1, 1, rankingGroup));
            rankingGroup.Rankings.Add(new CompetitionRanking(Guid.NewGuid(), team2, 2, rankingGroup));
            rankingGroup.Rankings.Add(new CompetitionRanking(Guid.NewGuid(), team3, 3, rankingGroup));
            rankingGroup.Rankings.Add(new CompetitionRanking(Guid.NewGuid(), team4, 4, rankingGroup));

            playoff.RankingGroups.Add(rankingGroup);

            var series1 = new BestOfPlayoffSeries
            {
                Round = 1,
                Playoff = playoff,
                Team1 = team1,
                Team2 = team2,
                RequiredWins = 4,
                Name = "Series 1"
            };

            var series2 = new BestOfPlayoffSeries
            {
                Round = 1,
                Playoff = playoff,
                Team1 = team3,
                Team2 = team4,
                RequiredWins = 4,
                Name = "Series 2"
            };


            playoff.Series.Add(series1);
            playoff.Series.Add(series2);

            while (!playoff.IsRoundComplete(playoff.CurrentRound))
            {
                var games = playoff.CreateGames();
                //var random = new Random(554211);
                var random = new Random();
                games.ToList().ForEach(g =>
                {
                    g.Play(random);
                    playoff.ProcessGame(g);
                    Console.WriteLine(PrintPlayoffGame((IPlayoffGame)g));
                });
            }


            Console.WriteLine(PrintSeries(series1));
            Console.WriteLine(PrintSeries(series2));
        }

        public static string PrintSeries(IPlayoffSeries series)
        {
            string format = "{0}. {1} - {2} : {3} - {4}";
            return string.Format(format, series.Name, series.Team1.Name, series.Team1Score, series.Team2Score, series.Team2.Name);
        }
        
        public static string PrintPlayoffGame(IPlayoffGame g)
        {
            return GameView.GetPlayoffGameSummaryView(new PlayoffGameSummaryViewModel(g.Identifier, g.Series.Name, g.Day, g.Year, g.Home.Identifier, g.Home.Name,
                        g.Away.Identifier, g.Away.Name, g.HomeScore, g.AwayScore, g.Complete));
        }
    }
}
