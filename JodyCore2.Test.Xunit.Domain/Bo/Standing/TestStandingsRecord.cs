using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Domain.Bo.Standing
{
    public class TestStandingsRecord
    {
        [Fact]
        public void ShouldGetGamesPlayed()
        {
            var record = new StandingsRecord(Guid.NewGuid(), null, null, "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.StrictEqual(71, record.GamesPlayed);
        }

        [Fact]
        public void ShouldGetWins()
        {
            var record = new StandingsRecord(Guid.NewGuid(), null, null, "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.StrictEqual(6, record.Wins);
        }

        [Fact]
        public void ShouldGetLoses()
        {
            var record = new StandingsRecord(Guid.NewGuid(), null, null, "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.StrictEqual(60, record.Loses);
        }

        [Fact]
        public void ShouldCalculatePoints()
        {
            int pointsMethod(IStandingsRecord r)
            {
                return r.Ties +
                    r.RegulationWins * 10 +
                    r.OverTimeWins * 100 +
                    r.ShootOutWins * 1000 +
                    r.RegulationLoses * 10000 +
                    r.OverTimeLoses * 100000 +
                    r.ShootoutLoses * 1000000;
            }

            var record = new StandingsRecord(Guid.NewGuid(), null, null, "None", 1, 2, 3, 4, 5, 6, 7, 25, 20, pointsMethod);

            Assert.StrictEqual(6543217, record.Points);
        }
    }
}
