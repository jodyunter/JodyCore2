using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class Standings:IStandings
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }        
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public string Description { get; set; }
        public string Division { get; set; }
        public virtual IList<IStandingsRecord> Records { get; set; }        

        public IStandingsRecord GetRecord(ITeam inputTeam)
        {
            return Records.ToList().Where(r => r.Team.Identifier == inputTeam.Identifier).FirstOrDefault();
        }

        public Standings() { }
        public Standings(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<IStandingsRecord> records)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;
            EndYear = endYear;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
            Division = division;
            Records = records;
        }
    }
}
