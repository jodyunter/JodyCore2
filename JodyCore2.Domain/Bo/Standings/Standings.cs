using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class Standings: Competition,ICompetition, IStandings, IBO
    {
        public string Division { get; set; }
        public virtual IList<IStandingsRecord> Records { get; set; }                

        public Standings():base(CompetitionType.Standings) { }

        public Standings(string name, int startYear, int startDay, int order, string description, string division, IList<IStandingsRecord> records, IList<ICompetitionRankingGroup> rankingGroups, bool setup, bool started, bool complete, bool processed)
            :this(Guid.NewGuid(), name, startYear, startDay, order, description, division, records, rankingGroups, setup, started, complete, processed)
        {
        }

        public Standings(Guid identifier, string name, int startYear, int startDay, int order, string description, string division, IList<IStandingsRecord> records, IList<ICompetitionRankingGroup> rankingGroups, bool setup, bool started, bool complete, bool processed)
            :base(identifier, name, startYear, startDay, order, description, setup, started, complete, processed, rankingGroups, CompetitionType.Standings)
        {
            Division = division;
            Records = records;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            this.DefaultProcessGame(game);
        }

        public override IList<ICompetitionGame> CreateGames(IList<ICompetitionGame> currentGames)
        {
            //standings games are created before standings competition starts
            //return an empty list so it can be procesed
            return new List<ICompetitionGame>();
        }

        public void SortGroups(Func<IStandings, IList<IStandingsRecord>> sortMethod)
        {
            var sortedRecords = sortMethod(this);

            //get a diciontary of team and sorted rank
            var dictionary = sortedRecords.ToDictionary(x => x.Team, y => 0);
            var rank = 1;

            //set the rank based on sorted records
            sortedRecords.ToList().ForEach(sr =>
            {
                dictionary[sr.Team] = rank;
                rank++;
            });

            //for each ranking group, set the rank based on the sorted records (may not start at one, or be continuous
            //then, for the ranking group, sort it and set the rank starting at 1
            RankingGroups.ToList().ForEach(rg =>
            {
                rg.Rankings.ToList().ForEach(ranking =>
                {
                    ranking.SetRank(dictionary[ranking.Team]);
                });

                int rank = 1;
                rg.Rankings.OrderBy(r => r.Rank).ToList().ForEach(r => 
                {
                    r.SetRank(rank);
                    rank++;
                });
            });
        }
        public override bool Equals(object obj)
        {
            return obj is Standings standings &&
                   base.Equals(obj) &&
                   Identifier.Equals(standings.Identifier) &&
                   Name == standings.Name &&
                   StartYear == standings.StartYear &&
                   StartDay == standings.StartDay &&
                   Order == standings.Order &&
                   Description == standings.Description &&
                   Setup == standings.Setup &&
                   Started == standings.Started &&
                   Complete == standings.Complete &&
                   Processed == standings.Processed &&
                   CompetitionType == standings.CompetitionType &&
                   Division == standings.Division;                   
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Identifier);
            hash.Add(Name);
            hash.Add(StartYear);
            hash.Add(StartDay);
            hash.Add(Order);
            hash.Add(Description);
            hash.Add(Setup);
            hash.Add(Started);
            hash.Add(Complete);
            hash.Add(Processed);
            hash.Add(CompetitionType);
            hash.Add(Division);            
            return hash.ToHashCode();
        }

        public override ICompetitionGame CreateGame(int year, int day, ITeam home, ITeam away)
        {
            return new CompetitionGame(this, year, day, home, away, true);
        }
    }
}
