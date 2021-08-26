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
    public class TestStandingsExtensions
    {
        [Test]
        public void ShouldUseDefaultGetPoints()
        {
            var record = new StandingsRecord(Guid.NewGuid(), null, null, "None", 1, 2, 3, 4, 5, 6, 7, 25, 20);
            Assert.AreEqual(19, record.Points);
        }

    }
}
