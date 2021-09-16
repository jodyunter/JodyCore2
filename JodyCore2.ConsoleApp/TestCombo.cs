using JodyCore2.ConsoleApp.Views;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using JodyCore2.Domain.Bo.Scheduling;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp
{
    public class TestCombo
    {

        public void run()
        {            
            var random = new Random();
            var team1 = new Team("Team 1", random.Next(6));
            var team2 = new Team("Team 2", random.Next(6));
            var team3 = new Team("Team 3", random.Next(6));
            var team4 = new Team("Team 4", random.Next(6));
            var team5 = new Team("Team 5", random.Next(6));
            var team6 = new Team("Team 6", random.Next(6));
            var team7 = new Team("Team 7", random.Next(6));
            var team8 = new Team("Team 8", random.Next(6));
            var team9 = new Team("Team 9", random.Next(6));
            var team10 = new Team("Team 10", random.Next(6));

            var teamList = new List<ITeam>() { team1, team2, team3, team4, team5, team6, team7, team8, team9, team10 };

            var standings = new Standings("Regular Season", 1, 1, 1, "Permier", "None", new List<IStandingsRecord>(), new List<ICompetitionRankingGroup>(), false, false, false, false);
            var playoffs = new Playoff("Playoff", 1, 1, 2, "Championship", new List<IPlayoffSeries>(), new List<ICompetitionRankingGroup>(), false, false, false, false);

            var overallGroup = new CompetitionRankingGroup(new List<ICompetition>() { standings, playoffs }, "Overall", null);
            teamList.ForEach(team => { overallGroup.AddRank(team, -1); });
            standings.RankingGroups.Add(overallGroup);
            playoffs.RankingGroups.Add(overallGroup);

            //standings groups

            //playoff groups
            var round2Group = new CompetitionRankingGroup(new List<ICompetition>() { playoffs }, "Round 2", null);
            var round3Group = new CompetitionRankingGroup(new List<ICompetition>() { playoffs }, "Round 3", null);

            playoffs.RankingGroups.Add(round2Group);
            playoffs.RankingGroups.Add(round3Group);

            teamList.ForEach(team =>
            {
                standings.Records.Add(new StandingsRecord(standings, team, team.Name));
            });

            var r1s1 = new BestOfPlayoffSeries(playoffs, "Series 1", 1, 4, null, null, overallGroup, 1, overallGroup, 8, round2Group, overallGroup, null, null, "1122121");
            var r1s2 = new BestOfPlayoffSeries(playoffs, "Series 2", 1, 4, null, null, overallGroup, 2, overallGroup, 7, round2Group, overallGroup, null, null, "1122121");
            var r1s3 = new BestOfPlayoffSeries(playoffs, "Series 3", 1, 4, null, null, overallGroup, 3, overallGroup, 6, round2Group, overallGroup, null, null, "1122121");
            var r1s4 = new BestOfPlayoffSeries(playoffs, "Series 4", 1, 4, null, null, overallGroup, 4, overallGroup, 5, round2Group, overallGroup, null, null, "1122121");

            var r2s1 = new BestOfPlayoffSeries(playoffs, "Series 5", 2, 4, null, null, round2Group, 1, round2Group, 4, round3Group, overallGroup, null, null, "1122121");
            var r2s2 = new BestOfPlayoffSeries(playoffs, "Series 6", 2, 4, null, null, round2Group, 2, round2Group, 3, round3Group, overallGroup, null, null, "1122121");

            var r3s1 = new BestOfPlayoffSeries(playoffs, "Series 7", 3, 4, null, null, round3Group, 1, round3Group, 2, null, null, null, null, "1122121");

            var round1 = new List<IPlayoffSeries>() { r1s1, r1s2, r1s3, r1s4 };
            var round2 = new List<IPlayoffSeries>() { r2s1, r2s2 };
            var round3 = new List<IPlayoffSeries>() { r3s1 };
            
            var series = new List<IPlayoffSeries>();
            series.AddRange(round1);
            series.AddRange(round2);
            series.AddRange(round3);

            playoffs.Series = series;

            standings.SetupCompetition();
            standings.StartCompetition();

            //schedule games
            var scheduledGames = Scheduler.ScheduleRoundRobin(1, 1, teamList, standings.CreateGame);
            var maxDay = scheduledGames.Keys.Max();

            var sg2 = Scheduler.ScheduleRoundRobin(1, scheduledGames.Keys.Max() + 1, teamList, standings.CreateGame);
            var sg3 = Scheduler.ScheduleRoundRobin(1, sg2.Keys.Max() + 1, teamList, standings.CreateGame);
            var sg4 = Scheduler.ScheduleRoundRobin(1, sg3.Keys.Max() + 1, teamList, standings.CreateGame);
            var sg5 = Scheduler.ScheduleRoundRobin(1, sg4.Keys.Max() + 1, teamList, standings.CreateGame);

            sg2.Keys.ToList().ForEach(k => scheduledGames[k] = sg2[k]);
            sg3.Keys.ToList().ForEach(k => scheduledGames[k] = sg3[k]);
            sg4.Keys.ToList().ForEach(k => scheduledGames[k] = sg4[k]);
            sg5.Keys.ToList().ForEach(k => scheduledGames[k] = sg5[k]);            

            while (!standings.Complete)
            {
                scheduledGames.Keys.OrderBy(k => k).ToList().ForEach(day =>
                {
                    Console.Clear();

                    scheduledGames[day].ToList().ForEach(g =>
                    {
                        var pg = (ICompetitionGame)g;
                        pg.Play(new Random());
                        pg.Competition.ProcessGame(pg);
                    });

                    standings.SortGroups(TestStandings.SortMethod);

                    var model = CompetitionMapper.StandingsToStandingsViewModel(standings, overallGroup);
                    Console.WriteLine(StandingsView.GetView(model));
                    Console.ReadLine();
                });

                bool complete = true;

                scheduledGames.Values.ToList().ForEach(dayOfGames =>
                {
                    var inCompleteGames = dayOfGames.Where(g => ((ICompetitionGame)g).Competition.Identifier == standings.Identifier && !((ICompetitionGame)g).Complete).Count();

                    if (inCompleteGames > 0) complete = false;                    
                });

                if (complete)
                {
                    standings.Complete = true;
                }
            }

            playoffs.SetupCompetition();
            playoffs.StartCompetition();

            var playoffGames = new List<IPlayoffGame>();
            var lastDayPlayed = 1;

            while (!playoffs.Complete)
            {                
                while (!playoffs.IsRoundComplete(playoffs.CurrentRound))
                {
                    Console.Clear();
                    //create needed games
                    var newGames = playoffs.CreateGames(playoffGames.ToList<ICompetitionGame>());
                    //schedule them
                    var newlyScheduledGames = Scheduler.ScheduleIndividualGames(newGames.ToList<IScheduleGame>(), playoffs.StartYear, lastDayPlayed + 1, playoffGames.ToList<IScheduleGame>());
                    playoffGames.AddRange(newlyScheduledGames.Select(a => (IPlayoffGame)a));

                    //get ready for next day to play
                    lastDayPlayed += 1;

                    //var random = new Random(554211);                    
                    var gameViews = new List<IGameSummaryViewModel>();

                    playoffGames.Where(sg => sg.Day == lastDayPlayed).ToList().ForEach(sg =>
                    {
                        sg.Play(random);
                        playoffs.ProcessGame(sg);
                        gameViews.Add(TestPlayoffs.GetPlayoffView(sg));
                    });

                    var model = CompetitionMapper.StandingsToStandingsViewModel(standings, overallGroup);
                    Console.WriteLine(StandingsView.GetView(model));

                    //Console.WriteLine(PrintGameDay(gameViews, lastDayPlayed));
                    Console.WriteLine(TestPlayoffs.PrintAllSeriesSummary(playoffs));
                    Console.ReadLine();
                }

                playoffs.ProcessEndOfCurrentRound();
            }
        }
    }
}
