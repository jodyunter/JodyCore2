using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IScheduleGameViewModel
    {
        Guid Identifier { get; set; }
        int Year { get; set; }
        int Day { get; set; }
        ITeam Home { get; set; }
        ITeam Away { get; set; }
    }
}
