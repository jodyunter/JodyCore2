using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competitions
{
    public class CompetitionGame : Game, ICompetitionGame
    {
        public ICompetition Competition { get; set; }


        public CompetitionGame() { }

        public CompetitionGame(Guid identifier, ICompetition competition, int day, int year, ITeam home, ITeam away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
            : base(identifier, day, year, home, away, homeScore, awayScore, complete, processed, canTie)
        {
            Competition = competition;
        }
    }
}
