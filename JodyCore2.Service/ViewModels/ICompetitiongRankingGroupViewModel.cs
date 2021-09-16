using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface ICompetitiongRankingGroupViewModel
    {
        Guid Identifier { get; }
        string Name { get; }
        IList<ISimpleCompetitionViewModel> Competitions { get; }
        IList<ICompetitionRankingViewModel> Rankings { get; }
    }

    public interface ICompetitionRankingViewModel
    {
        Guid Identifier { get; }        
        Guid GroupIdentifier { get; }
        string GroupName { get; }
        Guid TeamIdentifier { get; }
        string TeamName { get; }
        int Rank { get; }
    }
    public interface ISimpleCompetitionViewModel
    {
        Guid Identifier { get; }
        string Name { get; }
    }
}
