using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Scheduling
{
    public class ScheduleGame
    {        
        public Guid Identifier { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public Guid Home { get; set; }
        public Guid Away { get; set; }
    }
}
