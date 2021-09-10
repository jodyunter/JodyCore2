using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo
{
    public class Game : IGame, IBO
    {
        public Guid Identifier { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public virtual ITeam Home { get; set; }
        public virtual ITeam Away { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool Complete { get; set; }
        public bool Processed { get; set; }
        public bool CanTie { get; set; }        

        public Game() { }

        public Game(Guid identifier, int day, int year, ITeam home, ITeam away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
        {
            Identifier = identifier;
            Day = day;
            Year = year;
            Home = home;
            Away = away;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Complete = complete;
            Processed = processed;
        }

        public void Play(Random r)
        {
            var homeScore = r.Next(6);
            var awayScore = r.Next(6);

            while (!CanTie && (homeScore == awayScore))
            {
                var a = r.Next(10);
                var b = r.Next(10);

                if (a > b)
                    homeScore++;
                else if (b > a)
                    awayScore++;
            }

            HomeScore = homeScore;
            AwayScore = awayScore;

            Complete = true;
        }

        public void Process()
        {
            Processed = true;
        }
    }
}
