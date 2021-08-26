using JodyCore2.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace JodyCore2.Data
{
    public class JodyContext : DbContext
    {

        public JodyContext():base()
        {            
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionStringName = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var fileString = "";

            if (environment != null)
            {
                fileString = "." + environment;
            }
            var configuration = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile($"appsettings{fileString}.json")
                              .Build();

            var optionsBuilder = new DbContextOptionsBuilder();

            if (connectionStringName == null)
            {
                connectionStringName = "DefaultConnectionString";
            }

            var connectionString = configuration
                        .GetConnectionString(connectionStringName);
            
            if (!options.IsConfigured)
            {
                options.UseNpgsql(connectionString);
            }
        }
        
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<GameDto> Games { get; set; }
        public DbSet<StandingsDto> Standings { get; set; }        
        public DbSet<StandingsRecordDto> StandingsRecords { get; set; }
        public DbSet<RankingDto> Rankings { get; set; }
        public DbSet<RankingGroupDto> RankingGroups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamDto>().ToTable("Teams");
            modelBuilder.Entity<TeamDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GameDto>().ToTable("Games");
            modelBuilder.Entity<GameDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StandingsRecordDto>().ToTable("StandingsRecords");
            modelBuilder.Entity<StandingsRecordDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StandingsDto>().ToTable("Standings");
            modelBuilder.Entity<StandingsDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RankingDto>().ToTable("Rankings");
            modelBuilder.Entity<RankingDto>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RankingGroupDto>().ToTable("RankingGroups");
            modelBuilder.Entity<RankingGroupDto>().Property(p => p.Id).ValueGeneratedOnAdd();
        }
        
    }
        
}
