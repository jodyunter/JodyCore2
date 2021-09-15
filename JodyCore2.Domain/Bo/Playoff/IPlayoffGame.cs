using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public interface IPlayoffGame:ICompetitionGame, IScheduleGame
    {
        IPlayoffSeries Series { get; }
    }
}
