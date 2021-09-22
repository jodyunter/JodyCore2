using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class CompetitionRankingViewModel : RankingViewModel, ICompetitionRankingViewModel
    {

        public CompetitionRankingViewModel(Guid identifier, Guid groupIdentifier, string groupName, Guid teamIdentifier, string teamName, int rank)
            :base(identifier, groupIdentifier, groupName, teamIdentifier, teamName, rank)
        {

        }
    }
}
