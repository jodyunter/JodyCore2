using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using JodyCore2.Domain.Bo.Rankings;
using System.Collections.Generic;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;

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
        public DbSet<PlayoffGame> PlayoffGames { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Standings> Standings { get; set; }        
        public DbSet<Playoff> Playoffs { get; set; }
        public DbSet<StandingsRecord> StandingsRecords { get; set; }
        public DbSet<PlayoffSeries> PlayoffSeries { get; set; }
        public DbSet<BestOfPlayoffSeries> BestOfPlayoffSeries { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<RankingGroup> RankingGroups { get; set; }
        public DbSet<CompetitionRanking> CompetitionRankings { get; set; }
        public DbSet<CompetitionRankingGroup> CompetitionRankingGroups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            onTeamCreating(modelBuilder);
            onGameCreating(modelBuilder);
                                   
            onRankingCreating(modelBuilder);
            onRankingGroupCreating(modelBuilder);

            onCompetitionCreating(modelBuilder);
            onCompetitionRankingCreating(modelBuilder);
            onCompetitionRankingGroupCreating(modelBuilder);
            onCompetitionGameCreating(modelBuilder);

            onStandingsCreating(modelBuilder);
            onStandingsRecordCreating(modelBuilder);

            onPlayoffCreating(modelBuilder);
            onPlayoffSeriesCreating(modelBuilder);
            onPlayoffGameCreating(modelBuilder);


        }
        
        private void onCompetitionCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competition>().HasKey(s => s.Identifier);
            modelBuilder.Entity<Competition>().HasMany(s => (IList<CompetitionRankingGroup>)s.RankingGroups);
        }
        private void onTeamCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasKey(t => t.Identifier);
        }
        private void onStandingsCreating(ModelBuilder modelBuilder)
        {
            //eventually we'll need ranking groups here too to deal with sorting and stuff
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
            modelBuilder.Entity<CompetitionRankingGroup>().HasMany(t => (IList<Competition>)t.Competitions);
        }

        private void onCompetitionGameCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Competition)t.Competition);
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Team)t.Home);
            modelBuilder.Entity<CompetitionGame>().HasOne(t => (Team)t.Away);
        }

        public void onPlayoffCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playoff>().HasMany(t => (IList<PlayoffSeries>)t.Series);
            modelBuilder.Entity<Playoff>().HasMany(t => (IList<CompetitionRankingGroup>)t.RankingGroups);
        }

        private void onPlayoffSeriesCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayoffSeries>().HasKey(ps => ps.Identifier);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (Playoff)t.Playoff);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (Team)t.Team1);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (Team)t.Team2);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.Team1FromGroup);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.Team2FromGroup);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.WinnerGoesTo);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.WinnerRankFrom);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.LoserGoesTo);
            modelBuilder.Entity<PlayoffSeries>().HasOne(t => (CompetitionRankingGroup)t.LoserRankFrom);
        }
        private void onPlayoffGameCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayoffGame>().HasOne(t => (Playoff)t.Competition);
            modelBuilder.Entity<PlayoffGame>().HasOne(t => (PlayoffSeries)t.Series);
            modelBuilder.Entity<PlayoffGame>().HasOne(t => (Team)t.Home);
            modelBuilder.Entity<PlayoffGame>().HasOne(t => (Team)t.Away);            
        }
        
    }
        
}
