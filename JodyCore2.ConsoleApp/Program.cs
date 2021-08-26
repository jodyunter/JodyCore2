﻿using JodyCore2.ConsoleApp.Views;
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

            var teams = teamService.GetAll().ToList();

            var scheduledGames = schedulingService.CreateScheduleGames(1, 1, teams.Select(t => t.Identifier).ToList(), 1, true);

            scheduledGames.ToList().ForEach(g =>
            {
                gameService.Create(g.Year, g.Day, g.Home, g.Away);
            });

            for (int i = 0; i < 10; i++)
            {
                gameService.PlayGamesOnDay(1, i + 1);
            }

            gameService.GetGames(1, 1, 9).OrderBy(g => g.Day).ToList().ForEach(g =>
            {
                Console.WriteLine(GameView.GetGameSummaryView(g));
            });

            
            var a = standingsService.Create("First", 1, 1, 1, 200, "Test Me Out", "Permier", teams);

            standingsService.ProcessGames(a.Identifier, gameService.GetGames(1, 1, 100).Select(g => g.Identifier).ToList());

            
            var standings = standingsService.Sort(a.Identifier);
            
            Console.WriteLine(StandingsView.GetView(standings));

            Console.ReadLine();
        }
    }
}
