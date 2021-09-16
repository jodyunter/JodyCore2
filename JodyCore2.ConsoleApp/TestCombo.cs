using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using JodyCore2.Domain.Bo.Scheduling;
using JodyCore2.Domain.Bo.Standings;
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
            var team1 = new Team("Team 1", 5);
            var team2 = new Team("Team 2", 5);
            var team3 = new Team("Team 3", 5);
            var team4 = new Team("Team 4", 5);
            var team5 = new Team("Team 5", 5);
            var team6 = new Team("Team 6", 5);
            var team7 = new Team("Team 7", 5);
            var team8 = new Team("Team 8", 5);
            var team9 = new Team("Team 9", 5);
            var team10 = new Team("Team 10", 5);

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

            var r3s1 = new BestOfPlayoffSeries(playoffs, "Series 7", 3, 4, null, null, round3Group, 2, round3Group, 3, null, null, null, null, "1122121");

            var round1 = new List<IPlayoffSeries>() { r1s1, r1s2, r1s3, r1s4 };
            var round2 = new List<IPlayoffSeries>() { r2s1, r2s2 };
            var round3 = new List<IPlayoffSeries>() { r3s1 };

            playoffs.Series.ToList().AddRange(round1);
            playoffs.Series.ToList().AddRange(round2);
            playoffs.Series.ToList().AddRange(round3);

            standings.SetupCompetition();
            standings.StartCompetition();

            //schedule games
            var scheduledGames = Scheduler.ScheduleRoundRobin(1, 1, teamList, standings.CreateGame);

            while (!standings.Complete)
            {
                scheduledGames.Keys.OrderBy(k => k).ToList().ForEach(day =>
                {
                    scheduledGames[day].ToList().ForEach(g =>
                    {
                        var pg = (IPlayoffGame)g;
                        pg.Play(new Random());
                        pg.Competition.ProcessGame(pg);
                    });
                });

                bool complete = true;

                scheduledGames.Values.ToList().ForEach(dayOfGames =>
                {
                    var inCompleteGames = dayOfGames.Where(g => ((IPlayoffGame)g).Competition.Identifier == standings.Identifier && !((IPlayoffGame)g).Complete).Count();

                    if (inCompleteGames > 0) complete = false;                    
                });

                if (complete)
                {
                    standings.Complete = true;
                }
            }
        }
    }
}
