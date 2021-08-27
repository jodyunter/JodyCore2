using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competition
{
    //todo:  Generalize the playoff and standings as competitions
    //todo:  Rankings could be moved higher up as a competition ranking.
    public interface ICompetition
    {        
        Guid Identifier { get; set; }
        string Name { get; set; }
        int StartYear { get; set; }
        int EndYear { get; set; }
        int StartDay { get; set; }
        int EndDay { get; set; }
        string Description { get; set; }
    }
}
