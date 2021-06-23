using JodyCore2.Data;
using JodyCore2.Domain;
using System;
using System.Linq;

namespace JodyCore2.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            using (var context = new JodyContext())
            {
                context.Add(new Team() { Identifier = Guid.NewGuid(), Name = "Team 2", Skill = 12 });
                context.SaveChanges();
                context.Teams.ToList().ForEach(t =>
                {
                    Console.WriteLine(t.Name + "\t" + t.Identifier);
                });
            }
            Console.ReadLine();
        }
    }
}
