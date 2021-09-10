using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Domain.Bo.Rankings;
using JodyCore2.Domain.Bo.Competition;
using System.Collections.Generic;

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
        
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CompetitionGame> CompetitionGames { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Standings> Standings { get; set; }        
        public DbSet<StandingsRecord> StandingsRecords { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<RankingGroup> RankingGroups { get; set; }
        public DbSet<CompetitionRanking> CompetitionRankings { get; set; }
        public DbSet<CompetitionRankingGroup> CompetitionRankingGroups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Team>().HasKey(t => t.Identifier);
            onGameCreating(modelBuilder);
                                   
            onRankingCreating(modelBuilder);
            onRankingGroupCreating(modelBuilder);

            modelBuilder.Entity<Competition>().HasKey(s => s.Identifier);
            onCompetitionRankingCreating(modelBuilder);
            onCompetitionRankingGroupCreating(modelBuilder);
            onCompetitionGameCreating(modelBuilder);

            onStandingsCreating(modelBuilder);
            onStandingsRecordCreating(modelBuilder);

        }

        private void onStandingsCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Standings>().HasMany(s => (IList<StandingsRecord>)s.Records);
        }

        private void onCompetitionRankingCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<CompetitionRanking>().HasOne(r => (Team)r.Team);
            modelBuilder.Entity<CompetitionRanking>().HasOne(r => (CompetitionRankingGroup)r.Group);
        }
        private void onRankingCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ranking>().HasKey(r => r.Identifier);
            modelBuilder.Entity<Ranking>().HasOne(r => (Team)r.Team);
            modelBuilder.Entity<Ranking>().HasOne(r => (RankingGroup)r.Group);
        }
        private void onGameCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasKey(g => g.Identifier);
            modelBuilder.Entity<Game>().HasOne(g => (Team)g.Home);
            modelBuilder.Entity<Game>().HasOne(g => (Team)g.Away);
        }
        private void onRankingGroupCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RankingGroup>().HasKey(rg => rg.Identifier);
            modelBuilder.Entity<RankingGroup>().HasMany(rg => (IList<Ranking>)rg.Rankings);
        }
        private void onStandingsRecordCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StandingsRecord>().HasKey(sr => sr.Identifier);
            modelBuilder.Entity<StandingsRecord>().HasOne(sr => (Standings)sr.ParentStandings);
            modelBuilder.Entity<StandingsRecord>().HasOne(sr => (Team)sr.Team);
        }

        private void onCompetitionRankingGroupCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitionRankingGroup>().HasOne(t => (Competition)t.Competition);
        }

        private void onCompetitionGameCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Competition)t.Competition);
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Team)t.Home);
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Team)t.Away);
        }
        
    }
        
}
