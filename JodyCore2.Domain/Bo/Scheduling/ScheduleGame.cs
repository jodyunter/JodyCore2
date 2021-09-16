using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class ScheduleGame:IScheduleGame
    {        
        public Guid Identifier { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public ITeam Home { get; set; }
        public ITeam Away { get; set; }

        public ScheduleGame(int year, int day, ITeam home, ITeam away):this(Guid.NewGuid(), year, day, home, away)
        {
        }

        public ScheduleGame(Guid identifier, int year, int day, ITeam home, ITeam away)
        {
            Identifier = identifier;
            Year = year;
            Day = day;
            Home = home;
            Away = away;
        }

        public void SetYear(int year)
        {
            Year = year;
        }

        public void SetDay(int day)
        {
            Day = day;
        }
    }
}
