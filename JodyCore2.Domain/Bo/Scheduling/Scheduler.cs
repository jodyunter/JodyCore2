using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class Scheduler
    {
        public const int NO_TEAM_VALUE = -1;
            
        
        //only gets the games that are relevant
        public static Dictionary<int, IList<IScheduleGame>> GetGamesByDayAsDictionary(IList<IScheduleGame> currentGames, int startingDay)
        {
            var dictionary = new Dictionary<int, IList<IScheduleGame>>();

            currentGames.ToList().ForEach(cg =>
            {
                if (cg.Day >= startingDay)
                {
                    if (!dictionary.ContainsKey(cg.Day)) //we only care about days we can create games on
                    {
                        dictionary[cg.Day] = new List<IScheduleGame>();
                    }

                    dictionary[cg.Day].Add(cg);
                }
            });

            return dictionary;
        }

        //this assumes the games are a valid addition
        //we don't have comprehensive validation on schedules yet
        public static IList<IScheduleGame> ScheduleGamesAsDay(IList<IScheduleGame> games, int year, int startingDay, IList<IScheduleGame> currentGames)
        {
            var currentGamesDictionary = GetGamesByDayAsDictionary(currentGames, startingDay);

            var teamList = games.Select(a => a.Home).ToList();
            teamList.AddRange(games.Select(a => a.Away).ToList());

            var added = false;
            int currentDay = startingDay;
            while (!added)
            {
                if (!currentGamesDictionary.ContainsKey(currentDay))
                {
                    currentGamesDictionary[currentDay] = new List<IScheduleGame>();
                }

                var dayOfGames = currentGamesDictionary[currentDay];

                if (!DoTeamsPlayInList(dayOfGames, teamList))
                {
                    games.ToList().ForEach(g =>
                    {
                        g.SetDay(currentDay);
                        g.SetYear(year);
                    });
                    added = true;
                }

                currentDay++;
            }

            return games;

        }
        public static IList<IScheduleGame> ScheduleIndividualGames(IList<IScheduleGame> games, int year, int startingDay, IList<IScheduleGame> currentGames)
        {
            var currentGameDictionary = GetGamesByDayAsDictionary(currentGames, startingDay);

            games.ToList().ForEach(g =>
            {
                int dayToAddGameOn = startingDay;
                bool added = false;

                while (!added)
                {

                    if (!currentGameDictionary.ContainsKey(dayToAddGameOn))
                    {
                        currentGameDictionary[dayToAddGameOn] = new List<IScheduleGame>();
                    }

                    if (DoTeamsPlayInList(currentGameDictionary[dayToAddGameOn], new List<ITeam>() { g.Home, g.Away }))
                    {
                        dayToAddGameOn++;
                    }
                    else
                    {
                        g.SetDay(dayToAddGameOn);
                        g.SetYear(year);
                        currentGameDictionary[dayToAddGameOn].Add(g);
                        added = true;
                    }
                }
            });

            return games;
            
        }
        //make sure this gets tested
        public static Dictionary<int, IList<IScheduleGame>> ScheduleRoundRobin(int year, int startingDay, IList<ITeam> teams, Func<int, int, ITeam, ITeam, IScheduleGame> createGame)
        {
            int totalTeams = teams.Count;

            int extraMatches = totalTeams % 2;
            int totalDays = totalTeams - 1 + extraMatches; //a perfect schedule has each team playing everyday, but if it's odd number of teams, they get a day of rest.

            var games = new Dictionary<int, IList<IScheduleGame>>();

            //initialize the matrix
            int[,] matrix = SetupMatrix(totalTeams, extraMatches);

            var gameList = new Dictionary<int, IList<IScheduleGame>>();

            for (int day = startingDay; day < (startingDay + totalDays); day++)
            {
                if (!gameList.ContainsKey(day))
                {
                    gameList.Add(day, new List<IScheduleGame>());
                }                

                if (day != startingDay)
                {                    
                    matrix = IncrementMatrix(matrix, totalTeams);
                    
                }
                var newGames = CreateGamesFromMatrix(matrix, teams, year, day, createGame);
                //validate day of games

                games.Add(day, newGames);
            }

            return games;
        }

        public static IList<IScheduleGame> CreateGamesFromMatrix(int[,] matrix, IList<ITeam> teams, int year, int day, Func<int, int, ITeam, ITeam, IScheduleGame> createGame)
        {
            var games = new List<IScheduleGame>();
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 0] != NO_TEAM_VALUE)
                {
                    var home = teams[matrix[i, 0]];
                    var away = teams[matrix[i, 1]];

                    //this needs to be a passed in method                    
                    var game = createGame(year, day, home, away);

                    games.Add(game);
                }
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
            int currentTeam = 0;

            for (int i = 0; i < matches; i++)
            {
                if (i == 0 && useExtra)
                {
                    matrix[i, 0] = NO_TEAM_VALUE;
                    matrix[i, 1] = totalTeams - 1;
                }
                else if (i == 0 && !useExtra)
                {
                    matrix[i, 0] = currentTeam;
                    matrix[i, 1] = totalTeams - 1;
                    currentTeam++;
                }
                else
                {
                    matrix[i, 0] = currentTeam;
                    matrix[i, 1] = totalTeams - 1 - i;
                    currentTeam++;
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
            if (startingValue == NO_TEAM_VALUE)
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
        public static int[] CountOfGamesPlayedInList(IList<IScheduleGame> games, ITeam team)
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

        public static bool DoTeamsPlayInList(IList<IScheduleGame> games, IList<ITeam> teams)
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
        public static bool DoesTeamPlayInList(IList<IScheduleGame> games, ITeam team)
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

        public static bool DoesTeamPlayInGame(IScheduleGame game, ITeam team)
        {
            if (game.Home.Equals(team) || game.Away.Equals(team))
            {
                return true;
            }

            return false;
        }

    }
}
