using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using System;
using System.Collections.Generic;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Playoffs
{
    public class BestofPlaffSeriesTests
    {
        public static IEnumerable<object[]> GetDataForGetWinner()
        {
            var team1 = new Team(Guid.NewGuid(), "Team A", 5);
            var team2 = new Team(Guid.NewGuid(), "Team B", 5);

            yield return new object[] { 0, 4, 4, team1, team2, team2 };
            yield return new object[] { 1, 0, 1, team1, team2, team1 };

        }

        [Theory]
        [MemberData(nameof(GetDataForGetWinner))]
        public void ShouldGetWinner(int team1Score, int team2Score, int requiredWins, ITeam team1, ITeam team2, ITeam expectedWinner)
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.RequiredWins = requiredWins;

            series.Team1Score = team1Score;
            series.Team2Score = team2Score;

            series.Complete = true;

            Assert.Equal(expectedWinner, series.GetWinner());            
        }

        public static IEnumerable<object[]> GetDataForGetLoser()
        {
            var team1 = new Team(Guid.NewGuid(), "Team A", 5);
            var team2 = new Team(Guid.NewGuid(), "Team B", 5);

            yield return new object[] { 3, 0, 3, team1, team2, team2 };
            yield return new object[] { 0, 2, 2, team1, team2, team1 };

        }

        [Theory]
        [MemberData(nameof(GetDataForGetLoser))]
        public void ShouldGetLoser(int team1Score, int team2Score, int requiredWins, ITeam team1, ITeam team2, ITeam expectedLoser)
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.RequiredWins = requiredWins;

            series.Team1Score = team1Score;
            series.Team2Score = team2Score;

            series.Complete = true;

            Assert.Equal(expectedLoser, series.GetLoser());
        }

        [Fact]
        public void ShouldNotGetWinnerSeriesNotComplete()
        {
            var series = new BestOfPlayoffSeries();
            series.Complete = false;

            Assert.Null(series.GetWinner());
        }

        [Fact]
        public void ShouldNotGetLoserSeriesNotComplete()
        {
            var series = new BestOfPlayoffSeries();
            series.Complete = false;
            Assert.Null(series.GetLoser());
        }

        [Fact]
        public void ShouldNotGetWinnerCompleteButWrongScore()
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;            

            series.Team1Score = 2;
            series.Team2Score = 1;

            series.Complete = true;

            var exception = Assert.Throws<ApplicationException>(() => series.GetWinner());
            Assert.Equal(BestOfPlayoffSeries.NO_TEAM_HAS_REQUIRED_SCORE, exception.Message);
        }

        [Fact]
        public void ShouldNotGetLoserCompleteButWrongScore()
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;

            series.Team1Score = 2;
            series.Team2Score = 1;

            series.Complete = true;

            var exception = Assert.Throws<ApplicationException>(() => series.GetLoser());
            Assert.Equal(BestOfPlayoffSeries.NO_TEAM_HAS_REQUIRED_SCORE, exception.Message);
        }

        public static IEnumerable<object[]> GetDataForSetIsComplete()
        {            
            yield return new object[] { true, 4, 4, 3 };
            yield return new object[] { true, 4, 3, 4 };
        }

        [Theory]
        [MemberData(nameof(GetDataForSetIsComplete))]
        public void ShouldSetIsComplete(bool expectedComplete, int requiredWins, int team1Score, int team2Score)
        {
            var series = new BestOfPlayoffSeries();
            series.RequiredWins = requiredWins;
            series.Team1Score = team1Score;
            series.Team2Score = team2Score;
            series.Complete = !expectedComplete;

            Assert.StrictEqual(expectedComplete, series.IsComplete());
            Assert.StrictEqual(expectedComplete, series.IsComplete());
        }

        public static IEnumerable<object[]> GetDataForCreateGames()
        {
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);

            var series = new BestOfPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.Complete = false;
            series.RequiredWins = 2;
            series.HomeString = "";

            
            yield return new object[] { series, 0, 0, new List<IPlayoffGame>(), 2 }; 
            yield return new object[] { series, 1, 0, new List<IPlayoffGame>(), 1 };
            yield return new object[] { series, 2, 0, new List<IPlayoffGame>(), 0 };
            yield return new object[] { series, 0, 0, new List<IPlayoffGame>(), 2 };
            yield return new object[] { series, 0, 1, new List<IPlayoffGame>(), 1 };
            yield return new object[] { series, 0, 2, new List<IPlayoffGame>(), 0 };
            yield return new object[] { series, 1, 1, new List<IPlayoffGame>(), 1 };
            yield return new object[] { series, 2, 1, new List<IPlayoffGame>(), 0 };
            yield return new object[] { series, 1, 2, new List<IPlayoffGame>(), 0 };

            var inCompleteGame1 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 0, false, false, false);
            var inCompleteGame2 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 0, false, false, false);

            yield return new object[] { series, 0, 0, new List<IPlayoffGame>() { inCompleteGame1 }, 1 };
            yield return new object[] { series, 0, 0, new List<IPlayoffGame>() { inCompleteGame1, inCompleteGame2 }, 0 };

            yield return new object[] { series, 1, 0, new List<IPlayoffGame>() { inCompleteGame1 }, 0 };
            yield return new object[] { series, 1, 1, new List<IPlayoffGame>() { inCompleteGame1 }, 0 };

            var completeGame1 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 0, true, true, false);
            var completeGame2 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 0, true, true, false);

            yield return new object[] { series, 1, 0, new List<IPlayoffGame>() { completeGame1 }, 1 };
            yield return new object[] { series, 1, 1, new List<IPlayoffGame>() { completeGame1, completeGame1 }, 1 };
            yield return new object[] { series, 1, 1, new List<IPlayoffGame>() { completeGame1, inCompleteGame2 }, 0 };
        }

        [Theory]
        [MemberData(nameof(GetDataForCreateGames))]
        public void ShouldCreateGames(PlayoffSeries series, int team1Score, int team2Score, IList<IPlayoffGame> currentGames, int expectedNewGames)
        {
            series.Team1Score = team1Score;
            series.Team2Score = team2Score;            

            var newGames = series.CreateGames(currentGames);

            Assert.StrictEqual(expectedNewGames, newGames.Count);
        }

        [Fact]
        public void ShouldNotCreateGamesUnprocessedGamesExist()
        {
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);

            var series = new BestOfPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.Complete = false;
            series.RequiredWins = 2;
            series.HomeString = "";
            var inCompleteGame1 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 3, true, false, false);
            var currentGames = new List<IPlayoffGame>() { inCompleteGame1 };

            var exception = Assert.Throws<ApplicationException>(() => series.CreateGames(currentGames));
            Assert.Equal(BestOfPlayoffSeries.UNPROCESSED_GAMES_EXIST, exception.Message);
        }

        [Fact]
        public void ShouldNotCreateGamesSeriesComplete()
        {
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);

            var series = new BestOfPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.Complete = false;
            series.RequiredWins = 2;
            series.HomeString = "";
            series.Complete = true; //key value in this test

            var inCompleteGame1 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 0, 3, true, false, false);
            var currentGames = new List<IPlayoffGame>() { inCompleteGame1 };

            var exception = Assert.Throws<ApplicationException>(() => series.CreateGames(currentGames));
            Assert.Equal(BestOfPlayoffSeries.SERIES_COMPLETE_CANT_CREATE_GAMES, exception.Message);
        }

        public static IEnumerable<object[]> GetDataForProcessGame()
        {
            var team1 = new Team(Guid.NewGuid(), "Team 1", 5);
            var team2 = new Team(Guid.NewGuid(), "Team 2", 5);

            var series = new BestOfPlayoffSeries();
            series.RequiredWins = 3;

            series.Team1 = team1;
            series.Team2 = team2;

            var game1 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 5, 4, true, false, false);
            var game2 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 5, 4, true, false, false);
            var game3 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team2, team1, 5, 4, true, false, false);
            var game4 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team2, team1, 5, 4, true, false, false);
            var game5 = new PlayoffGame(Guid.NewGuid(), null, series, 1, 1, team1, team2, 5, 4, true, false, false);

            yield return new object[] { series, game1, 1, 0, false };
            yield return new object[] { series, game2, 2, 0, false };
            yield return new object[] { series, game3, 2, 1, false };
            yield return new object[] { series, game4, 2, 2, false };
            yield return new object[] { series, game5, 3, 2, true };            
        }

        [Theory]
        [MemberData(nameof(GetDataForProcessGame))]
        public void ShouldProcessGame(PlayoffSeries series, IPlayoffGame gameToProcess, int expectedTeam1Score, int expectedTeam2Score, bool expectedCompleteValue)
        {

            series.ProcessGame(gameToProcess);

            Assert.StrictEqual(series.Team1Score, expectedTeam1Score);
            Assert.StrictEqual(series.Team2Score, expectedTeam2Score);
            Assert.StrictEqual(expectedCompleteValue, series.Complete);
            Assert.True(gameToProcess.Processed);
        }

        [Fact]
        public void ShouldNotProcessGameSeriesComplete()
        {
            var badseries = new BestOfPlayoffSeries();

            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;
            series.Complete = true;
            series.Team1Score = 2;
            series.Team2Score = 1;

            var game = new PlayoffGame(Guid.NewGuid(), null, series, 1, 5, series.Team1, series.Team2, 5, 3, true, false, true);          

            var exception = Assert.Throws<ApplicationException>(() => series.ProcessGame(game));
            Assert.Equal(BestOfPlayoffSeries.SERIES_COMPLETE_CANT_PROCESS_GAMES, exception.Message);
        }

        [Fact]
        public void ShouldNotProcessGameNotGameForSeries()
        {
            var badseries = new BestOfPlayoffSeries();

            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;

            series.Team1Score = 2;
            series.Team2Score = 1;

            var game = new PlayoffGame(Guid.NewGuid(), null, badseries, 1, 5, series.Team1, series.Team2, 5, 3, true, false, true);


            var exception = Assert.Throws<ApplicationException>(() => series.ProcessGame(game));
            Assert.Equal(BestOfPlayoffSeries.WRONG_SERIES_FOR_GAME, exception.Message);
        }

        [Fact]
        public void ShouldNotProcessGameInComplete()
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;

            series.Team1Score = 2;
            series.Team2Score = 1;

            var game = new PlayoffGame(Guid.NewGuid(), null, series, 1, 5, series.Team1, series.Team2, 5, 3, false, false, true);


            var exception = Assert.Throws<ApplicationException>(() => series.ProcessGame(game));
            Assert.Equal(BestOfPlayoffSeries.GAME_INCOMPLETE_OR_PROCESSED, exception.Message);
        }

        [Fact]
        public void ShouldNotProcessGameAlreadyProcessed()
        {
            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;

            series.Team1Score = 2;
            series.Team2Score = 1;

            var game = new PlayoffGame(Guid.NewGuid(), null, series, 1, 5, series.Team1, series.Team2, 5, 3, true, true, true);


            var exception = Assert.Throws<ApplicationException>(() => series.ProcessGame(game));
            Assert.Equal(BestOfPlayoffSeries.GAME_INCOMPLETE_OR_PROCESSED, exception.Message);
        }
    }
}
