using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface ICompetitiongRankingGroupViewModel:IRankingGroupViewModel
    {
        IList<ISimpleCompetitionViewModel> Competitions { get; }        
    }

    public interface ICompetitionRankingViewModel:IRankingViewModel
    {        
    }
    public interface ISimpleCompetitionViewModel
    {
        Guid Identifier { get; }
        string Name { get; }
    }
}
