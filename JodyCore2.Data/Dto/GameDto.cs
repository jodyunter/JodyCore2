using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class GameDto:IBaseDto
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public TeamDto Home { get; set; }
        public TeamDto Away { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool Complete { get; set; }
        public bool Processed { get; set; }
        public bool CanTie { get; set; }

        public GameDto() { }

        public GameDto(Guid identifier, int day, int year, TeamDto home, TeamDto away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
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
    }
}
