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
        Guid Home { get; set; }
        Guid Away { get; set; }
    }
}
