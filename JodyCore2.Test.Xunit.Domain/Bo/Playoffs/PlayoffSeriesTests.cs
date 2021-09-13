using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Playoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Playoffs
{
    public class PlayoffSeriesTests
    {
        public static IEnumerable<object[]> GetDataForHomeAwayTest()
        {
            var team1 = new Team(Guid.NewGuid(), "Team A", 5);
            var team2 = new Team(Guid.NewGuid(), "Team B", 5);
            var homeString = "12221";

            yield return new object[] { team1, team2, 1, team1, team2, homeString };
            yield return new object[] { team1, team2, 2, team2, team1, homeString };
            yield return new object[] { team1, team2, 3, team2, team1, homeString };
            yield return new object[] { team1, team2, 4, team2, team1, homeString };
            yield return new object[] { team1, team2, 5, team1, team2, homeString };
            yield return new object[] { team1, team2, 6, team2, team1, homeString };
            yield return new object[] { team1, team2, 7, team1, team2, homeString };
            yield return new object[] { team1, team2, 1, team1, team2, "" };
            yield return new object[] { team1, team2, 2, team2, team1, "" };
        }

        [Theory]
        [MemberData(nameof(GetDataForHomeAwayTest))]
        public void ShouldCreateGameWithProperHomeAndAwayTeam(ITeam team1, ITeam team2, int gameNumber, ITeam expectedHomeTeam, ITeam expectedAwayTeam, string homeAwayString)
        {
            var series = new TestPlayoffSeries();
            series.Team1 = team1;
            series.Team2 = team2;
            series.HomeString = homeAwayString;

            var homeTeam = series.GetHomeTeamForGame(gameNumber);

            Assert.Equal(expectedHomeTeam, homeTeam);
            Assert.NotEqual(expectedAwayTeam, homeTeam);
            

        }
    }
}
