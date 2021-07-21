using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public class Standings:IStandings
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public string Description { get; set; }
        public string Division { get; set; }
        public IList<IStandingsRecord> Records { get; set; }

        public Action<IGame> ProcessGame { get; set; }

        public IStandingsRecord GetRecord(ITeam inputTeam)
        {
            return Records.ToList().Where(r => r.Team.Identifier == inputTeam.Identifier).FirstOrDefault();
        }
    }
}
