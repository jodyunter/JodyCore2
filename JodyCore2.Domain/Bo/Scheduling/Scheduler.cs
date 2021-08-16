using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class Scheduler
    {
        public IList<ScheduleGame> ScheduleGames(int year, int startingDay, IList<ITeam> teams)
        {
            return null;
        }

        public Dictionary<int, IList<ScheduleGame>> ScheduleRoundRobin(int year, int startingDay, IList<Guid> teams)
        {
            int totalTeams = teams.Count;

            int extraMatches = totalTeams % 2;
            int totalDays = totalTeams - 1 + extraMatches; //a perfect schedule has each team playing everyday, but if it's odd number of teams, they get a day of rest.

            var games = new Dictionary<int, IList<ScheduleGame>>();

            //initialize the matrix
            int[,] matrix = SetupMatrix(totalTeams, extraMatches);

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
                var newGames = CreateGamesFromMatrix(matrix, teams, year, i);
                //validate day of games

                games.Add(i, newGames);
            }

            return games;
        }

        public IList<ScheduleGame> CreateGamesFromMatrix(int[,] matrix, IList<Guid> teams, int year, int day)
        {
            var games = new List<ScheduleGame>();
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var home = teams[matrix[i, 0]];
                var away = teams[matrix[i, 1]];

                var game = new ScheduleGame(Guid.NewGuid(), year, day, home, away);

                games.Add(game);
            }

            return games;

        }
        //does not allow for more than a single extra match
        public  static int[,] SetupMatrix(int totalTeams, int extraMatches)
        {
            bool useExtra = extraMatches > 0;

            int matches = (totalTeams / 2) + extraMatches;

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

            return matrix;
        }

        public static int[,] IncrementMatrix(int[,] matrix, int totalTeams)
        {
            var newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = IncrementPosition(totalTeams, matrix[i, j]);
                }
            }

            return newMatrix;
        }
        public static int IncrementPosition(int totalTeams, int startingValue)
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
            count[1] = 0;

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
            bool aTeamPlaysinList = false;

            teams.ToList().ForEach(t =>
            {
                var teamPlaysInLlist = Scheduler.DoesTeamPlayInList(games, t);
                if (teamPlaysInLlist)
                    aTeamPlaysinList = true;
            });

            return aTeamPlaysinList;
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
