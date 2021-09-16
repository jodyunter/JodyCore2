using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class CompetitionRankingViewModel : ICompetitionRankingViewModel
    {
        public Guid Identifier { get; set; }        

        public Guid GroupIdentifier { get; set; }

        public string GroupName { get; set; }

        public Guid TeamIdentifier { get; set; }

        public string TeamName { get; set; }

        public int Rank { get; set; }

        public CompetitionRankingViewModel(Guid identifier, Guid groupIdentifier, string groupName, Guid teamIdentifier, string teamName, int rank)
        {
            Identifier = identifier;            
            GroupIdentifier = groupIdentifier;
            GroupName = groupName;
            TeamIdentifier = teamIdentifier;
            TeamName = teamName;
            Rank = rank;
        }
    }
}
