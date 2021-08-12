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

        //home,away
        public static int[] CountOfGamesPlayedInList(IList<ScheduleGame> games, Guid team)
        {
            int[] count = new int[2];

            count[0] = 0;
            count[1] = 1;

            games.ToList().ForEach(g =>
            {
                if (g.Home.Equals(team))
                {
                    count[0]++;
                }

                if (g.Away.Equals(team))
                {
                    count[1]++;
                }
            });

            return count;

        }

        public static bool DoTeamsPlayInList(IList<ScheduleGame> games, IList<Guid> teams)
        {
            return false;
        }
        //TODO: Test these!
        public static bool DoesTeamPlayInList(IList<ScheduleGame> games, Guid team)
        {
            bool doesPlay = false;

            for (int i = 0; (i < games.Count) && !doesPlay; i++)            
            {
                if (DoesTeamPlayInGame(games[i], team))
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
