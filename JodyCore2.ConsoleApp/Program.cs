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

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });

            Console.ReadLine();
        }
    }
}
