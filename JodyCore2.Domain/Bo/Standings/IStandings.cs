using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public interface IStandings:ICompetition
    {
        string Division { get; set; }
        IList<IStandingsRecord> Records { get; set; }
        IStandingsRecord GetRecord(ITeam inputTeam)
        {
            return Records.Where(t => t.Team.Identifier.Equals(inputTeam.Identifier)).First();
        }        
                
                
    }
}
