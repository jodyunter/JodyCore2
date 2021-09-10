using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class Standings: Competitions.Competition, IStandings, IBO
    {
        public string Division { get; set; }
        public virtual IList<IStandingsRecord> Records { get; set; }                

        public Standings():base(CompetitionType.Standings) { }
        public Standings(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<IStandingsRecord> records)
            :base(identifier, name, startYear, endYear, startDay, endDay, description, CompetitionType.Standings)
        {
 
            Division = division;
            Records = records;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            this.DefaultProcessGame(game);
        }
    }
}
