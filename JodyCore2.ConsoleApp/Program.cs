using JodyCore2.ConsoleApp.Views;
using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Service;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JodyCore2.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("CONNECTION_STRING", "consoleConnectionString");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "console");            

            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            var services = new Services();
            var teamService = services.TeamService;
            var gameService = services.GameService;
            var schedulingService = services.SchedulingService;
            var standingsService = services.StandingsService;

            for (int i = 0; i < 10; i++)
            {
                teamService.Create("Team " + i, 5);
            }

            var allTeams = teamService.GetAll().ToList();
            var teams = allTeams.GetRange(3, 5);

            var scheduledGames1 = schedulingService.CreateScheduleGames(1, 1, teams.Select(t => t.Identifier).ToList(), 1, true);
            var lastDay = scheduledGames1.Max(d => d.Day);
            var scheduledGames2 = schedulingService.CreateScheduleGames(1, lastDay + 1, teams.Select(t => t.Identifier).ToList(), 1, true);


            var scheduledGames = new List<IScheduleGameViewModel>();
            scheduledGames.AddRange(scheduledGames1);
            scheduledGames.AddRange(scheduledGames2);

            var a = standingsService.Create("First", 1, 1, 1, 200, "Test Me Out", "Permier", teams);

            scheduledGames.ToList().ForEach(g =>
            {
                standingsService.CreateStandingsGame(a.Identifier, g.Year, g.Day, g.Home, g.Away);
            });

            for (int i = 0; i < 20; i++)
            {
                gameService.PlayGamesOnDay(1, i + 1);
            }

            standingsService.GetStandingsGames(a.Identifier, 1, 1, 9).OrderBy(g => g.Day).ToList().ForEach(g =>
            {
                Console.WriteLine(GameView.GetGameSummaryView(g));
            });
                       

            standingsService.ProcessGames(a.Identifier);

            
            var standings = standingsService.Sort(a.Identifier);
            
            Console.WriteLine(StandingsView.GetView(standings));

            Console.ReadLine();
        }
    }
}
