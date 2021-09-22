using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class CompetitionRankingGroupViewModel : RankingGroupViewModel, ICompetitiongRankingGroupViewModel
    {

        public IList<ISimpleCompetitionViewModel> Competitions { get; set; }


        public CompetitionRankingGroupViewModel(Guid identifier, string name, IList<ISimpleCompetitionViewModel> competitions, IList<ICompetitionRankingViewModel> rankings)
            : base(identifier, name, rankings.ToList<IRankingViewModel>())
        {
            Competitions = competitions;         
        }
    }
}
