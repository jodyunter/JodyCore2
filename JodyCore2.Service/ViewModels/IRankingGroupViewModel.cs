using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IRankingGroupViewModel
    {
         
        Guid Identifier { get; }
        string Name { get; }        
        IList<IRankingViewModel> Rankings { get; }
    }

    public interface IRankingViewModel
    {
        Guid Identifier { get; }
        Guid GroupIdentifier { get; }
        string GroupName { get; }
        Guid TeamIdentifier { get; }
        string TeamName { get; }
        int Rank { get; }
    }
}

