﻿using JodyCore2.ConsoleApp.Views;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using JodyCore2.Domain.Bo.Scheduling;
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

            var finalGroup = new CompetitionRankingGroup(Guid.NewGuid(), playoff, "Final", new List<ICompetitionRanking>());            

            playoff.RankingGroups.Add(rankingGroup);

            var series1 = new BestOfPlayoffSeries(Guid.NewGuid(), playoff, "Series 1", 1, 1, null, null,
                            rankingGroup, 1, rankingGroup, 4, 
                            finalGroup, rankingGroup, null, null,
                            new List<IPlayoffGame>(), 0, 0, "", false, false);
            
            var series2 = new BestOfPlayoffSeries(Guid.NewGuid(), playoff, "Series 2", 1, 1, null, null,
                            rankingGroup, 2, rankingGroup, 3,
                            finalGroup, rankingGroup, null, null,
                            new List<IPlayoffGame>(), 0, 0, "", false, false);

            var series3 = new BestOfPlayoffSeries(Guid.NewGuid(), playoff, "Series 3", 2, 2, null, null,
                            finalGroup, 1, finalGroup, 2,
                            null, null, null, null,
                            new List<IPlayoffGame>(), 0, 0, "", false, false);

            playoff.Series.Add(series1);
            playoff.Series.Add(series2);
            playoff.Series.Add(series3);

            playoff.SetupCompetition();
            playoff.StartCompetition();

            var scheduledGames = new Dictionary<int, IList<IScheduleGame>>();
            var lastDayPlayed = 0;

            while (!playoff.Complete)
            {
                while (!playoff.IsRoundComplete(playoff.CurrentRound))
                {
                    //create needed games
                    var games = playoff.CreateGames();
                    //schedule them
                    Scheduler.AddGamesToSchedule(games.ToList<IScheduleGame>(), playoff.StartYear, lastDayPlayed + 1, scheduledGames);
                    
                    //get ready for next day to play
                    lastDayPlayed += 1;

                    //var random = new Random(554211);
                    var random = new Random();
                    scheduledGames[lastDayPlayed].ToList().ForEach(sg =>
                    {
                        var g = (IPlayoffGame)sg;
                        g.Play(random);
                        playoff.ProcessGame(g);
                        Console.WriteLine(PrintPlayoffGame(g));
                    });

                    Console.ReadLine();
                }

                playoff.ProcessEndOfCurrentRound();
            }

            Console.WriteLine(PrintSeries(series1));
            Console.WriteLine(PrintSeries(series2));
            Console.WriteLine(PrintSeries(series3));
        }

        public static string PrintSeries(IPlayoffSeries series)
        {
            string format = "R{5} {0}. {1} - {2} : {3} - {4}";
            return string.Format(format, series.Name, series.Team1.Name, series.Team1Score, series.Team2Score, series.Team2.Name, series.Round);
        }
        
        public static string PrintPlayoffGame(IPlayoffGame g)
        {
            return GameView.GetPlayoffGameSummaryView(new PlayoffGameSummaryViewModel(g.Identifier, g.Series.Identifier, g.Series.Name, g.Series.Round, g.Day, g.Year, g.Home.Identifier, g.Home.Name,
                        g.Away.Identifier, g.Away.Name, g.HomeScore, g.AwayScore, g.Complete));
        }
    }
}
