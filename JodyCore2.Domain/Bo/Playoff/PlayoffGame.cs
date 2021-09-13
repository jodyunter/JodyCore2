using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Playoff
{
    public class PlayoffGame : CompetitionGame, IPlayoffGame
    {
        public IPlayoffSeries Series { get; set; }

        public PlayoffGame():base() { }

        public PlayoffGame(Guid identifier, ICompetition competition, IPlayoffSeries series, int day, int year, ITeam home, ITeam away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
            : base(identifier, competition, day, year, home, away, homeScore, awayScore, complete, processed, canTie)
        {
            Series = series;
        }
    }
}
