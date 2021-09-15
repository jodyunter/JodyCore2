using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class ScheduleGameViewModel:ScheduleGame, IScheduleGameViewModel
    {
        public ScheduleGameViewModel(Guid identifier, int year, int day, ITeam home, ITeam away) : base(identifier, year, day, home, away)
        { }
    }
}
