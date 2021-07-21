using JodyCore2.Domain;
using JodyCore2.Domain.Bo.Standings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Domain.Bo.Standings
{
    public class TestStandingsRecord
    {
        [Test]
        public void ShouldGetGamesPlayed()
        {
            var record = new StandingsRecord(1, "None", "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.AreEqual(71, record.GamesPlayed);
        }

        [Test]
        public void ShouldGetWins()
        {
            var record = new StandingsRecord(1, "None", "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.AreEqual(6, record.Wins);
        }

        [Test]
        public void ShouldGetLoses()
        {
            var record = new StandingsRecord(1, "None", "None", 1, 2, 3, 10, 20, 30, 5, 25, 20, null);

            Assert.AreEqual(60, record.Loses);
        }

        [Test]
        public void ShouldCalculatePoints()
        {
            int pointsMethod (IStandingsRecord r)
            {
                return r.Ties +
                    r.RegulationWins * 10 +
                    r.OverTimeWins * 100 +
                    r.ShootOutWins * 1000 +
                    r.RegulationLoses * 10000 +
                    r.OverTimeLoses * 100000 +
                    r.ShootoutLoses * 1000000;                
            }

            var record = new StandingsRecord(1, "None", "None", 1, 2, 3, 4, 5, 6, 7, 25, 20, pointsMethod);

            Assert.AreEqual(6543217, record.Points);
        }
    }
}
