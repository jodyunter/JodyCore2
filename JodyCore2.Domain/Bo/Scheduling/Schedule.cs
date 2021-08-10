using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class Schedule
    {
        public int Year { get; set; }
        public IDictionary<int, IList<ScheduleGame>> Games { get; set; }

        //need something to deal with existing vs new games
        //current idea is to have the games with an identifier not be created
        public void AddDayOfGamesToSchedule(IList<ScheduleGame> games, int dayToStartAt)
        {
            int day = -1;

            if (games.Count == 0)
            {
                throw new ApplicationException("No Games to Schedule.");
            }
            else
            {
                day = games[0].Day;
            }

            int gamesNotOnDay = games.Select(g => g.Day != day).ToList().Count;

            if (gamesNotOnDay > 0)
            {
                throw new ApplicationException("Some games are not on the same day.");
            }
            //check if the day itself is valid

            if (Games.ContainsKey(dayToStartAt))
            {
                //check if games can be added to a day
            }
            else
            {
                //set the games in the list to the appropriate day
                games.ToList().ForEach(g => g.Day = dayToStartAt);
                //add the games to the schedule
                Games.Add(dayToStartAt, games);                
            }
        }

        //TODO: Test these!
        public static bool DoesTeamPlayInList(IList<ScheduleGame> games, Guid team)
        {
            bool doesPlay = false;

            for (int i = 0; (i < games.Count) && !doesPlay; i++)            
            {
                if (DoesTeamPlayInGame(g, team))
                {
                    doesPlay = true;
                }
            }

            return doesPlay;
        }

        public static bool DoesTeamPlayInGame(ScheduleGame game, Guid team)
        {
            if (game.Home.Equals(team) || game.Away.Equals(team))
            {
                return true;
            }

            return false;
        }
    }
     
}
