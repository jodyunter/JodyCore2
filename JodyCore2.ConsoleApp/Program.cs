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
            /*
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
            
            for (int i = 0; i < 21; i++)
            {
                teamService.Create("Team " + i, 5);
            }

            var allTeams = teamService.GetAll().ToList();
            var teams = allTeams.GetRange(10, 11);
            var teams2 = allTeams.GetRange(0, 10);

            var scheduledGames1 = schedulingService.CreateScheduleGames(1, 1, teams.Select(t => t.Identifier).ToList(), 1, true);
            var lastDay = scheduledGames1.Max(d => d.Day);
            var scheduledGames2 = schedulingService.CreateScheduleGames(1, 1, teams2.Select(t => t.Identifier).ToList(), 1, true);

            
            var a = standingsService.Create("First", 1, 1, 1, 200, "Test Me Out", "Permier", teams);
            var b = standingsService.Create("Second", 1, 1, 1, 200, "Test Me Out", "Lower", teams2);

            scheduledGames1.ToList().ForEach(g =>
            {
                standingsService.CreateStandingsGame(a.Identifier, g.Year, g.Day, g.Home, g.Away);
            });

            scheduledGames2.ToList().ForEach(g =>
            {
                standingsService.CreateStandingsGame(b.Identifier, g.Year, g.Day, g.Home, g.Away);
            });


            for (int i = 0; i < 22; i++)
            {
                gameService.PlayGamesOnDay(1, i + 1);
            }            
                       
            standingsService.ProcessGames(a.Identifier);
            standingsService.ProcessGames(b.Identifier);


            var standings = standingsService.Sort(a.Identifier);
            var standings2 = standingsService.Sort(b.Identifier);

            Console.WriteLine(StandingsView.GetView(standings));
            Console.WriteLine(StandingsView.GetView(standings2));
            */

            //var test = new TestPlayoffs();
            var test = new TestStandings();
            test.run();

            Console.ReadLine();


        }
    }
}
