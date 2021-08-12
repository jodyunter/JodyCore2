using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class Scheduler
    {
        public IList<ScheduleGame> ScheduleGames(int startingDay, IList<ITeam> teams)
        {
            return null;
        }

        public IList<ScheduleGame> ScheduleRoundRobin(int startingDay, IList<Guid> teams)
        {
            int totalTeams = teams.Count;

            int extraMatches = totalTeams % 2;
            bool useExtra = extraMatches > 0;

            int matches = (totalTeams / 2) + extraMatches;
            int totalDays = totalTeams - 1 + extraMatches; //a perfect schedule has each team playing everyday, but if it's odd number of teams, they get a day of rest.

            //initialize the matrix
            int[,] matrix = new int[matches, 2];

            /*
             * [0, 5]
             * [1, 4]
             * [2, 3]
             */
            for (int i = 0; i < matches; i++)
            {
                if (i == 0 && useExtra)
                {
                    matrix[i, 0] = -1;
                    matrix[i, 1] = totalTeams - 1;
                }
                else if (i == 0 && !useExtra)
                {
                    matrix[i, 0] = 0;
                    matrix[i, 1] = totalTeams - 1;
                }
                else
                {
                    matrix[i, 0] = i;
                    matrix[i, 1] = totalTeams - 1 - i;                        
                }
            }

            var gameList = new Dictionary<int, IList<ScheduleGame>>();

            for (int i = startingDay; i <= (startingDay + totalDays); i++)
            {
                if (!gameList.ContainsKey(startingDay))
                {
                    gameList.Add(startingDay, new List<ScheduleGame>());
                }                

                if (i != startingDay)
                {
                    IncrementMatrix(matrix, totalTeams);
                    
                }
                var games = null; //CreateGamesFromMatrix(matrix, teams);
            }

        }

        public int[,] IncrementMatrix(int[,] matrix, int totalTeams)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = IncrementPosition(totalTeams, matrix[i, j]);
                }
            }

            return matrix;
        }
        public int IncrementPosition(int totalTeams, int startingValue)
        {
            if (startingValue == -1)
            {
                return startingValue;
            }
            else
            {
                startingValue++;
                if (startingValue >= totalTeams)
                {
                    return 0;
                }
                else
                {
                    return startingValue;
                }
            }
        }
        //some validation functions

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
