using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Scheduling
{
    public class SchedulerTests
    {
        [Fact]
        public void ShouldScheduleGames()
        {
            var team1 = new Team("Team 1", 5);
            var team2 = new Team( "Team 2", 5);
            var team3 = new Team("Team 3", 5);
            var team4 = new Team("Team 4", 5);
            var team5 = new Team("Team 5", 5);
            var team6 = new Team("Team 6", 5);

            var day1 = new List<IScheduleGame>() 
            { 
                new ScheduleGame(1, 1, team1, team2),
                new ScheduleGame(1, 1, team3, team4),
                new ScheduleGame(1, 1, team5, team6),
            };

            var day2 = new List<IScheduleGame>()
            {
                new ScheduleGame(1, 2, team1, team3),
                new ScheduleGame(1, 2, team3, team5),
                new ScheduleGame(1, 2, team2, team6),
            };

            var day3 = new List<IScheduleGame>()
            {
                new ScheduleGame(1, 3, team1, team4),
                new ScheduleGame(1, 3, team2, team5),
                new ScheduleGame(1, 3, team3, team6),
            };

            var newGames = new List<IScheduleGame>()
            {
                new ScheduleGame(-1, -1, team1, team2),
                new ScheduleGame(-1, -1, team2, team3)
        };

            var currentGames = new List<IScheduleGame>();
            currentGames.AddRange(day1);
            currentGames.AddRange(day2);
            currentGames.AddRange(day3);

            var scheduledGames = Scheduler.ScheduleIndividualGames(newGames, 1, 3, currentGames);

            Assert.StrictEqual(2, scheduledGames.Count);
            Assert.StrictEqual(4, scheduledGames.Where(s => s.Identifier == newGames[0].Identifier).First().Day);
            Assert.StrictEqual(5, scheduledGames.Where(s => s.Identifier == newGames[1].Identifier).First().Day);
        }

        [Theory]
        [InlineData(1, 10, 2)]
        [InlineData(-1, 25, -1)]
        [InlineData(-1, 16, -1)]
        [InlineData(5, 10, 6)]
        [InlineData(10, 10, 0)]
        [InlineData(9, 10, 0)]
        public void ShouldIncrementPosition(int currentvalue, int totalTeams, int expectedResult)
        {
            var result = Scheduler.IncrementPosition(totalTeams, currentvalue);
            Assert.StrictEqual(expectedResult, result);
        }

        public static IEnumerable<object[]> GetDataForIncrementalMatrix()
        {
            //even number of teams
            yield return new object[] {
                new int[,]
                { {0, 5 },
                  {1, 4 },
                  {2, 3 } },
                new int[,]
                { {1, 0 },
                  {2, 5 },
                  {3, 4 } },
                6
            };
            //odd number of teams
            yield return new object[] {
                new int[,]
                { {-1, 4 },
                  {0, 3 },
                  {1, 2 } },
                new int[,]
                { {-1, 0 },
                  {1, 4 },
                  {2, 3 } },
                5
            };
        }

        [Theory]
        [MemberData(nameof(GetDataForIncrementalMatrix))]
        public void ShouldIncrementMatrix(int[,] inputMatrix, int[,] expectedOutputMatrix, int totalTeams)
        {
            var resultMatrix = Scheduler.IncrementMatrix(inputMatrix, totalTeams);

            isMatrixEqaul(inputMatrix, expectedOutputMatrix, resultMatrix);
        }

        private void isMatrixEqaul(int[,] inputMatrix, int[,] expectedMatrix, int[,] resultMatrix)
        {            
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {                    
                    Assert.StrictEqual(expectedMatrix[i, j], resultMatrix[i, j]);
                }
            }
        }

        public static IEnumerable<object[]> GetDataForCountTests()
        {
            var teamList = new List<ITeam>()
            {
                new Team(Guid.NewGuid(), "Team 1", 5),
                new Team(Guid.NewGuid(), "Team 2", 5),
                new Team(Guid.NewGuid(), "Team 3", 5),
                new Team(Guid.NewGuid(), "Team 4", 5),
                new Team(Guid.NewGuid(), "Team 5", 5),
                new Team(Guid.NewGuid(), "Team 6", 5),
                new Team(Guid.NewGuid(), "Team 7", 5),
                new Team(Guid.NewGuid(), "Team 8", 5),
                new Team(Guid.NewGuid(), "Team 9", 5)
            };

            var games = new List<ScheduleGame>()
            {
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[0], teamList[1]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[0], teamList[2]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[0], teamList[3]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[2], teamList[4]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[2], teamList[5]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[5], teamList[2]),
                new ScheduleGame(Guid.NewGuid(), 1, 1, teamList[6], teamList[4])
            };

            yield return new object[] { teamList, games, 0, 3, 0 };
            yield return new object[] { teamList, games, 1, 0, 1 };
            yield return new object[] { teamList, games, 2, 2, 2 };
            yield return new object[] { teamList, games, 3, 0, 1 };
            yield return new object[] { teamList, games, 4, 0, 2 };
            yield return new object[] { teamList, games, 5, 1, 1 };
            yield return new object[] { teamList, games, 6, 1, 0 };
            yield return new object[] { teamList, games, 7, 0, 0 };
            yield return new object[] { teamList, games, 8, 0, 0 };
        }

        [Theory]
        [MemberData(nameof(GetDataForCountTests))]
        public void ShouldCountTeamsInList(IList<ITeam> teamList, IList<IScheduleGame> games, int teamPositionToCheck, int expectedHome, int expectedAway)
        {

            var counts = Scheduler.CountOfGamesPlayedInList(games, teamList[teamPositionToCheck]);

            Assert.StrictEqual(expectedHome, counts[0]);
            Assert.StrictEqual(expectedAway, counts[1]);

        }
        public static IEnumerable<object[]> GetDataForDoesTeamPlayInGame()
        {
            var guid1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var guid2 = new Team(Guid.NewGuid(), "Team 2", 5);
            var guid3 = new Team(Guid.NewGuid(), "Team 3", 5);

            var game = new ScheduleGame(Guid.NewGuid(), 1, 1, guid1, guid2);

            yield return new object[] { game, guid1, true }; //home team
            yield return new object[] { game, guid2, true }; //away team
            yield return new object[] { game, guid3, false }; //no team
        }

        [Theory]
        [MemberData(nameof(GetDataForDoesTeamPlayInGame))]
        public void TestDoTeamsPlayInGame(IScheduleGame game, ITeam team, bool expectedResult)
        {
            Assert.Equal(expectedResult, Scheduler.DoesTeamPlayInGame(game, team));
        }

        public static IEnumerable<object[]> GetDataForDoesTeamPlayInList()
        {
            var guid1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var guid2 = new Team(Guid.NewGuid(), "Team 2", 5);
            var guid3 = new Team(Guid.NewGuid(), "Team 3", 5);
            var guid4 = new Team(Guid.NewGuid(), "Team 4", 5);
            var guid5 = new Team(Guid.NewGuid(), "Team 5", 5);
            var guid6 = new Team(Guid.NewGuid(), "Team 6", 5);

            var games = new List<IScheduleGame>()
            {
                new ScheduleGame(Guid.NewGuid(), 1, 1, guid1, guid2),
                new ScheduleGame(Guid.NewGuid(), 1, 1, guid3, guid4)
            };

            yield return new object[] { games, guid1, true };
            yield return new object[] { games, guid2, true };
            yield return new object[] { games, guid3, true };
            yield return new object[] { games, guid4, true };
            yield return new object[] { games, guid5, false };
            yield return new object[] { games, guid6, false };

        }

        [Theory]
        [MemberData(nameof(GetDataForDoesTeamPlayInList))]
        public void TestDoesTeamPlayInList(IList<IScheduleGame> games, ITeam team, bool expectedResults)
        {
            Assert.Equal(expectedResults, Scheduler.DoesTeamPlayInList(games, team));
        }

        public static IEnumerable<object[]> GetDataForDoesATeamPlayInList()
        {
            var guid1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var guid2 = new Team(Guid.NewGuid(), "Team 2", 5);
            var guid3 = new Team(Guid.NewGuid(), "Team 3", 5);
            var guid4 = new Team(Guid.NewGuid(), "Team 4", 5);
            var guid5 = new Team(Guid.NewGuid(), "Team 5", 5);
            var guid6 = new Team(Guid.NewGuid(), "Team 6", 5);

            var games = new List<ScheduleGame>()
            {
                new ScheduleGame(Guid.NewGuid(), 1, 1, guid1, guid2),
                new ScheduleGame(Guid.NewGuid(), 1, 1, guid3, guid4)
            };

            yield return new object[] { games, new List<ITeam>() { guid1, guid2, guid3, guid4, guid5, guid6 }, true };
            yield return new object[] { games, new List<ITeam>() { guid1, guid2, guid3 }, true };
            yield return new object[] { games, new List<ITeam>() { guid5, guid6 }, false };

        }

        [Theory]
        [MemberData(nameof(GetDataForDoesATeamPlayInList))]
        public void TestDoesATeamPlayInLilst(IList<IScheduleGame> games, IList<ITeam> teams, bool expectedResults)
        {
            Assert.Equal(expectedResults, Scheduler.DoTeamsPlayInList(games, teams));
        }
        
        public static IEnumerable<object[]> GetDataForMatrixSetup()
        {
            yield return new object[] { 3, 1, new int[,] { { -1, 2 }, { 0, 1 } } };
            yield return new object[] { 4, 0, new int[,] { { 0, 3 }, { 1, 2 } } };
        }

        [Theory]
        [MemberData(nameof(GetDataForMatrixSetup))]
        public void ShouldSetupMatrix(int totalTeams, int extraMatches, int[,] expectedResult)
        {
            var matrix = Scheduler.SetupMatrix(totalTeams, extraMatches);

            isMatrixEqaul(expectedResult, expectedResult, matrix);
        }

        public static IEnumerable<object[]> GetDataForCreateGamesFromMatrix()
        {
            var teams = new List<ITeam>()
            {
                new Team(Guid.NewGuid(), "Team 1", 5),
                new Team(Guid.NewGuid(), "Team 2", 5),
                new Team(Guid.NewGuid(), "Team 3", 5),
                new Team(Guid.NewGuid(), "Team 4", 5),
                new Team(Guid.NewGuid(), "Team 5", 5),
                new Team(Guid.NewGuid(), "Team 6", 5),
                new Team(Guid.NewGuid(), "Team 7", 5),
                new Team(Guid.NewGuid(), "Team 8", 5),
                new Team(Guid.NewGuid(), "Team 9", 5),
                new Team(Guid.NewGuid(), "Team 10", 5)
            };

            yield return new object[]
            {
                new int[,] 
                { 
                    { 0, 1 },
                    { 2, 3 },
                    { 4, 5 }
                }, teams,
                new List<IScheduleGame>()
                {
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[0], teams[1]),
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[2], teams[3]),
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[4], teams[5]),
                },
                3
            };

            yield return new object[]
            {
                new int[,]
                {
                    {Scheduler.NO_TEAM_VALUE, 1 },
                    {5, 9 },
                    {8, 3 },
                    {2, 5 }
                }, teams,
                new List<IScheduleGame>()
                {
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[5], teams[9]),
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[8], teams[3]),
                    new ScheduleGame(Guid.NewGuid(), 5, 2, teams[2], teams[5])
                },
                3
            };
        }

        private IScheduleGame CreateGame(int year, int day, ITeam home, ITeam away)
        {
            return new ScheduleGame(year, day, home, away);
        }
        [Theory]
        [MemberData(nameof(GetDataForCreateGamesFromMatrix))]
        public void ShouldCreateGamesFromMatrix(int[,] matrix, IList<ITeam> teams, IList<IScheduleGame> expectedGames, int count)
        {
            var games = Scheduler.CreateGamesFromMatrix(matrix, teams, 5, 2, CreateGame);

            games.ToList().ForEach(g =>
            {
                Assert.NotNull(expectedGames.ToList().Select(eg => eg.Home.Equals(g.Home) && eg.Away.Equals(g.Away)).DefaultIfEmpty());
            });

            Assert.StrictEqual(count, games.Count);
            //test
        }   
        
        [Fact]
        public void ShouldCreateRoundRobin()
        {
            var teams = new List<ITeam>()
            {
                new Team(Guid.NewGuid(), "Team 1", 5),
                new Team(Guid.NewGuid(), "Team 2", 5),
                new Team(Guid.NewGuid(), "Team 3", 5),
                new Team(Guid.NewGuid(), "Team 4", 5),
                new Team(Guid.NewGuid(), "Team 5", 5),
                new Team(Guid.NewGuid(), "Team 6", 5),
                new Team(Guid.NewGuid(), "Team 7", 5),
                new Team(Guid.NewGuid(), "Team 8", 5),
                new Team(Guid.NewGuid(), "Team 9", 5),
                new Team(Guid.NewGuid(), "Team 10", 5)
            };

            int expectedDays = 9;

            //specific number of days
            //specific number of games
            //no duplicate teams on a day
            //result should be exact and consistent, write out the schedule results

            var result = Scheduler.ScheduleRoundRobin(1, 1, teams, CreateGame);

            //expected days
            Assert.StrictEqual(expectedDays, result.Count);

            //need way more validation here!
            Assert.True(false);
            
        }
    }
}
