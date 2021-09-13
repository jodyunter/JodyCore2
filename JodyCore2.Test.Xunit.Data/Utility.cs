using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data
{
    public static class Utility
    {

        public static IStandings CreateStandingsNoRecords()
        {
            return new Standings(Guid.NewGuid(), "Standings Name", 1, 1, 1, 250, "Test", "Test this", new List<IStandingsRecord>());
        }
    }
}
