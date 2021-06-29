using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IGameSummaryViewModel
    {
        Guid Identifier { get; }
        int Day { get; }
        int Year { get; }
        Guid HomeTeamIdentifier { get; }
        String HomeTeamName { get; }
        Guid AwayTeamIdentifier { get; }
        String AwayTeamName { get; }
        int HomeScore { get; }
        int AwayScore { get; }
        bool Complete { get; }
        

        
    }
}
