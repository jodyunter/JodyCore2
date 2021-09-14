using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IPlayoffGameSummaryViewModel:IGameSummaryViewModel
    {
        Guid SeriesIdentifier { get; }
        string Series { get;  }
        int Round { get; }
    }
}
