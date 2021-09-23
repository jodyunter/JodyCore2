using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public interface IRankingService
    {
        IRankingViewModel CreateRanking(IRankingGroupViewModel group, ITeamViewModel team, int initialRank);
        IRankingGroupViewModel CreateRankingGroup(string name, IList<ITeamViewModel> teams, int defaultRank = 1);
        ICompetitiongRankingGroupViewModel CreateCompetitionRakingGroupFromRankingGroup(Guid rankingGroupIdentifier, IList<Guid> competitionIdentifierList);
    }
}
