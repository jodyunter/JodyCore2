using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Standings
{
    public static class StandingsExtensions
    {

        public static void DefaultProcessGame(this IStandings standings, IGame game)
        {

        }

        public static int DefaultGetPoints(this IStandingsRecord record, IStandingsRecord thatRecord)
        {
            return record.Wins * 2 + record.Ties;
        }
    }
}
