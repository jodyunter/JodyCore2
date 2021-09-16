using JodyCore2.ConsoleApp.Views;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Scheduling;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp
{
    public class TestStandings
    {
        public void run()
        {
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);
            var team3 = new Team(Guid.NewGuid(), "Team 3", 5);
            var team4 = new Team(Guid.NewGuid(), "Team 4", 5);
            var team5 = new Team(Guid.NewGuid(), "Team 5", 5);
            var team6 = new Team(Guid.NewGuid(), "Team 6", 5);
            var team7 = new Team(Guid.NewGuid(), "Team 7", 5);
            var team8 = new Team(Guid.NewGuid(), "Team 8", 5);
            var team9 = new Team(Guid.NewGuid(), "Team 9", 5);
            var team10 = new Team(Guid.NewGuid(), "Team 10", 5);
            var team11 = new Team(Guid.NewGuid(), "Team 11", 5);
            var team12 = new Team(Guid.NewGuid(), "Team 12", 5);

            var teamList = new List<ITeam>() { team1, team2, team3, team4, team5, team6, team7, team8, team9, team10, team11, team12 };
            var division1List = new List<ITeam>() { team1, team2, team3, team4, team5, team6 };
            var division2List = new List<ITeam>() { team7, team8, team9, team10, team11, team12 };

            var standings = new Standings("My Standings", 1, 1, 1, "My Description", "No Division",null, null, false, false, false, false);            
            
            var competitionList = new List<ICompetition>() { standings };
            
            var leagueGroup = new CompetitionRankingGroup(competitionList, "Overall", new List<ICompetitionRanking>());
            teamList.ForEach(team => { leagueGroup.AddRank(team, -1); });
            var division1Group = new CompetitionRankingGroup(competitionList, "Division 1", new List<ICompetitionRanking>());
            division1List.ForEach(team => { division1Group.AddRank(team, -1); });
            var division2Group = new CompetitionRankingGroup(competitionList, "Division 2", new List<ICompetitionRanking>());
            division2List.ForEach(team => { division2Group.AddRank(team, -1); });
            var rankingGroups = new List<ICompetitionRankingGroup>() { leagueGroup, division1Group, division2Group };            

            var standingsRecords = new List<IStandingsRecord>();
            teamList.ForEach(team => { standingsRecords.Add(new StandingsRecord(standings, team, team.Name)); });

            standings.RankingGroups = rankingGroups;
            standings.Records = standingsRecords;

            int gamesVsAll = 2;
            int gamesVsDivision = 2;
            var games = new List<IScheduleGame>();

            int dayToStart = 1;

            for (int i = 0; i < gamesVsAll; i++)
            {
                var gamesToAdd = Scheduler.ScheduleRoundRobin(1, dayToStart, teamList, standings.CreateGame);

                gamesToAdd.Values.ToList().ForEach(dayOfGames =>
                {
                    var scheduledGames = Scheduler.ScheduleGamesAsDay(dayOfGames, 1, dayToStart, games);
                    games.AddRange(scheduledGames);
                });                
            }
            
            //validate schedule
            for (int i = 0; i < gamesVsDivision; i++)
            {
                var gamesToAdd = Scheduler.ScheduleRoundRobin(1, dayToStart, division1List, standings.CreateGame);

                gamesToAdd.Values.ToList().ForEach(dayOfGames =>
                {
                    var scheduledGames = Scheduler.ScheduleGamesAsDay(dayOfGames, 1, dayToStart, games);
                    games.AddRange(scheduledGames);
                });

                gamesToAdd = Scheduler.ScheduleRoundRobin(1, dayToStart, division2List, standings.CreateGame);
                gamesToAdd.Values.ToList().ForEach(dayOfGames =>
                {
                    var scheduledGames = Scheduler.ScheduleGamesAsDay(dayOfGames, 1, dayToStart, games);
                    games.AddRange(scheduledGames);
                });
            }

            var dictionary = Scheduler.GetGamesByDayAsDictionary(games, 1);

            dictionary.Keys.OrderBy(k => k).ToList().ForEach(day =>
            {
                Console.Clear();
                var toPlay = dictionary[day];
                toPlay.ToList().ForEach(g =>
                {
                    var cg = (ICompetitionGame)g;
                    cg.Play(new Random());
                    standings.ProcessGame(cg);
                });

                standings.SortGroups(SortMethod);

                var model = StandingsMapper.StandingsToStandingsViewModel(standings, leagueGroup);
                Console.WriteLine(StandingsView.GetView(model));

                var div1 = StandingsMapper.StandingsToStandingsViewModel(standings, division1Group);
                Console.WriteLine(StandingsView.GetView(div1));

                var div2 = StandingsMapper.StandingsToStandingsViewModel(standings, division2Group);
                Console.WriteLine(StandingsView.GetView(div2));

                Console.ReadLine();
            });

            Console.WriteLine("Done");
            Console.ReadLine();

        }

        public static IList<IStandingsRecord> SortMethod(IStandings standings)
        {
            return standings.Records.ToList().OrderByDescending(r => r.Points)
                            .ThenBy(r => r.GamesPlayed)
                            .ThenByDescending(r => r.Wins)
                            .ThenByDescending(r => r.GoalDifference)
                            .ThenByDescending(r => r.GoalsFor).ToList();
        }
        
        
    }
}
