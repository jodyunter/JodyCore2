using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public interface IScheduleGame
    {
        public Guid Identifier { get; }
        public int Year { get; }
        public int Day { get; }
        public ITeam Home { get; }
        public ITeam Away { get; }
        void SetYear(int year);
        void SetDay(int day);
    }
}
