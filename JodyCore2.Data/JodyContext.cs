using JodyCore2.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data
{
    public class JodyContext : DbContext
    {

        public JodyContext():base()
        {            
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json")
                              .Build();

            var optionsBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration
                        .GetConnectionString("DefaultConnection");

            if (!options.IsConfigured)
            {
                options.UseNpgsql(connectionString);
            }
        }
        
        public DbSet<Team> Teams { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Team>().Property(p => p.Id).ValueGeneratedOnAdd();

        }
        
    }
        
}
