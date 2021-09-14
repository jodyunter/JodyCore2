using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public interface IPlayoff:ICompetition
    {
        IList<IPlayoffSeries> Series { get; }        
        void ProcessGame(IPlayoffGame game);
    }
}
