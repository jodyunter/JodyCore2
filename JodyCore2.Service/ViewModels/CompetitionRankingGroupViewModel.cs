using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class CompetitionRankingGroupViewModel : ICompetitiongRankingGroupViewModel
    {
        public Guid Identifier { get; set; }

        public string Name { get; set; }

        public IList<ISimpleCompetitionViewModel> Competitions { get; set; }

        public IList<ICompetitionRankingViewModel> Rankings { get; set; }

        public CompetitionRankingGroupViewModel(Guid identifier, string name, IList<ISimpleCompetitionViewModel> competitions, IList<ICompetitionRankingViewModel> rankings)
        {
            Identifier = identifier;
            Name = name;
            Competitions = competitions;
            Rankings = rankings;
        }
    }
}
