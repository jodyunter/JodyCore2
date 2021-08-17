using JodyCore2.Domain.Bo.Scheduling;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Mappers
{
    public static class ScheduleGameMapper
    {
        public static IScheduleGameViewModel ScheduleGameToScheduleGameViewModel(ScheduleGame game)
        {
            return new ScheduleGameViewModel(game.Identifier, game.Year, game.Day, game.Home, game.Away);
        }
    }
}
