using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class Standings: Competition, IStandings, IBO
    {
        public string Division { get; set; }
        public virtual IList<IStandingsRecord> Records { get; set; }                

        public Standings():base(CompetitionType.Standings) { }

        public Standings(Guid identifier, string name, int startYear, int startDay, int order, string description, string division, IList<IStandingsRecord> records, bool setup, bool started, bool complete, bool processed)
            :base(identifier, name, startYear, startDay, order, description, setup, started, complete, processed, CompetitionType.Standings)
        {
            Division = division;
            Records = records;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            this.DefaultProcessGame(game);
        }

        public override IList<ICompetitionGame> CreateGames()
        {
            //standings games are created before standings competition starts
            //return an empty list so it can be procesed
            return new List<ICompetitionGame>();
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
    }
}
