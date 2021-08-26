using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public interface IStandings
    {
        Guid Identifier { get; set; }
        string Name { get; set; }
        int StartYear { get; set; }
        int EndYear { get; set; }
        int StartDay { get; set; }
        int EndDay { get; set; }
        string Description { get; set; }
        string Division { get; set; }
        IList<IStandingsRecord> Records { get; set; }
        IStandingsRecord GetRecord(ITeam inputTeam);
    }
}
