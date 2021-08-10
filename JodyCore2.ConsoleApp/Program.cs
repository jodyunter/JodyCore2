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

            var teamService = new TeamService(new TeamRepository(), new GameRepository());

            
            teamService.Create("My Name", 5);            

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });

            var team = teamService.GetAll().ToList().Where(t => t.Name == "My Name").FirstOrDefault();

            teamService.Save(team.Identifier, "New Name", 25);

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });

            Console.ReadLine();
        }
    }
}
