using JodyCore2.ConsoleApp.Views;
using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Service;
using JodyCore2.Service.ViewModels;
using System;
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
            
            for (int i = 0; i < 10; i++)
            {
                teamService.Create("Team " + i, 5);
            }

            var teams = teamService.GetAll().ToList();

            var gameService = services.GameService;
            gameService.Create(1, 1, teams[0].Identifier, teams[1].Identifier);
            gameService.Create(1, 1, teams[2].Identifier, teams[3].Identifier);
            gameService.Create(1, 1, teams[4].Identifier, teams[5].Identifier);
            gameService.Create(1, 1, teams[6].Identifier, teams[7].Identifier);
            gameService.Create(1, 1, teams[8].Identifier, teams[9].Identifier);

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });


            gameService.PlayGamesOnDay(1, 1);

            gameService.GetGames(1, 1, 1).ToList().ForEach(g =>
            {
                Console.WriteLine(GameView.GetGameSummaryView(g));
            });

            Console.ReadLine();
        }
    }
}
