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


        public CompetitionGame():base() { }

        public CompetitionGame(Guid identifier, ICompetition competition, int day, int year, ITeam home, ITeam away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
            : base(identifier, day, year, home, away, homeScore, awayScore, complete, processed, canTie)
        {
            Competition = competition;
        }

        public override bool Equals(object obj)
        {
            return obj is CompetitionGame game &&
                   base.Equals(obj) &&                   
                   Competition.Identifier == game.Competition.Identifier;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Identifier);
            hash.Add(Day);
            hash.Add(Year);
            hash.Add(Home);
            hash.Add(Away);
            hash.Add(HomeScore);
            hash.Add(AwayScore);
            hash.Add(Complete);
            hash.Add(Processed);
            hash.Add(CanTie);
            hash.Add(Competition);
            return hash.ToHashCode();
        }
    }
}
