using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void ShouldCreateGames()
        {
            throw new NotImplementedException();
        }

        public void ShouldProcessGame()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldNotProcessGameSeriesComplete()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldNotProcessGameNotPlayoffGame()
        {
            var badseries = new BestOfPlayoffSeries();

            var series = new BestOfPlayoffSeries();
            series.Team1 = new Team(Guid.NewGuid(), "Test 1", 5);
            series.Team2 = new Team(Guid.NewGuid(), "Test 2", 5);
            series.RequiredWins = 3;

            series.Team1Score = 2;
            series.Team2Score = 1;

            var game = new CompetitionGame(Guid.NewGuid(), null, 1, 5, series.Team1, series.Team2, 5, 3, true, false, true);


            var exception = Assert.Throws<ApplicationException>(() => series.ProcessGame(game));
            Assert.Equal(BestOfPlayoffSeries.NOT_PLAYOFF_GAME, exception.Message);
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
