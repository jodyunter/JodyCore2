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

        //need to create a Game Rules class to handle things like Can Tie
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
            CanTie = canTie;
        }

        public void SetYear(int year)
        {
            Year = year;
        }

        public void SetDay(int day)
        {
            Day = day;
        }

        public void Play(Random r)
        {
            var multiplier = Home.Skill < Away.Skill? -1: 1;

            var diff = r.Next(Math.Abs(Home.Skill - Away.Skill)) * multiplier;

            var homeScore = r.Next(6 + diff);
            var awayScore = r.Next(6 - diff);

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

        public ITeam GetWinner()
        {
            if (Complete)
            {
                if (HomeScore > AwayScore)
                {
                    return Home;
                }
                else if (HomeScore < AwayScore)
                {
                    return Away;
                }
            }

            return null;
        }

        public ITeam GetLoser()
        {
            if (Complete)
            {
                if (HomeScore < AwayScore)
                {
                    return Home;
                }
                else if (HomeScore > AwayScore)
                {
                    return Away;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            return obj is Game game &&
                   Identifier.Equals(game.Identifier) &&
                   Day == game.Day &&
                   Year == game.Year &&
                   EqualityComparer<ITeam>.Default.Equals(Home, game.Home) &&
                   EqualityComparer<ITeam>.Default.Equals(Away, game.Away) &&
                   HomeScore == game.HomeScore &&
                   AwayScore == game.AwayScore &&
                   Complete == game.Complete &&
                   Processed == game.Processed &&
                   CanTie == game.CanTie;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
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
            return hash.ToHashCode();
        }
    }
}
