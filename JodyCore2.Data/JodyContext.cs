using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

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
        
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<GameDto> Games { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamDto>().ToTable("Teams");
            modelBuilder.Entity<TeamDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GameDto>().ToTable("Games");
            modelBuilder.Entity<GameDto>().Property(p => p.Id).ValueGeneratedOnAdd();

        }
        
    }
        
}
