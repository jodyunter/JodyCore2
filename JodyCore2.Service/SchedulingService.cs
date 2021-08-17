using JodyCore2.Domain.Bo.Scheduling;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class SchedulingService:ISchedulingService
    {
        public IList<IScheduleGameViewModel> CreateScheduleGames(int year, int startingDay, IList<Guid> teams, int rounds, bool homeAndAway)
        {
            var games = Scheduler.ScheduleRoundRobin(year, startingDay, teams);

            var listOfGames = new List<IScheduleGameViewModel>();

            games.Keys.ToList().ForEach(k =>
            {
                listOfGames.AddRange(games[k].Select(g => ScheduleGameMapper.ScheduleGameToScheduleGameViewModel(g)));
            });


            return listOfGames;
        }
    }
}
