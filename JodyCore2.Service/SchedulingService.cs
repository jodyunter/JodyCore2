using JodyCore2.Domain.Bo;
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
        public IList<IScheduleGameViewModel> CreateScheduleGames(int year, int startingDay, IList<ITeam> teams, int rounds, bool homeAndAway)
        {
            var games = Scheduler.ScheduleRoundRobin(year, startingDay, teams);

            var listOfGames = new List<IScheduleGameViewModel>();

            games.Keys.ToList().ForEach(k =>
            {
                listOfGames.AddRange(games[k].Select(g => ScheduleGameMapper.ScheduleGameToScheduleGameViewModel(g)));
            });
            
            //todo refactor to it's own method
            if (homeAndAway)
            {
                var newGames = new Dictionary<int, IList<ScheduleGame>>();

                int nextDay = games.Keys.Max();
                nextDay += 1;

                games.Keys.ToList().ForEach(key =>
                {
                    var listOfGames = games[key];
                    var newListOfGames = new List<ScheduleGame>();
                    listOfGames.ToList().ForEach(sg =>
                    {
                        newListOfGames.Add(new ScheduleGame(Guid.NewGuid(), year, nextDay, sg.Away, sg.Home));
                    });

                    newGames[nextDay] = newListOfGames;
                    nextDay++;
                });

                newGames.Keys.ToList().ForEach(k =>
                {
                    listOfGames.AddRange(newGames[k].Select(g => ScheduleGameMapper.ScheduleGameToScheduleGameViewModel(g)));
                });
            }

            return listOfGames;
        }
    }
}
