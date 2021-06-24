using JodyCore2.Data;
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
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            var teamService = new TeamService();

            var team = new TeamViewModel(Guid.NewGuid(), "My Name", 5);
            teamService.Save(team);

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });

            team.Name = "New Name";
            team.Skill = 25;

            teamService.Save(team);

            teamService.GetAll().ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name + "\t" + t.Identifier + "\t" + t.Skill);
            });

            Console.ReadLine();
        }
    }
}
