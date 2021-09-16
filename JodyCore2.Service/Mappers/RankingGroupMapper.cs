using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Mappers
{
    public class RankingGroupMapper
    {
        public ICompetitiongRankingGroupViewModel CompetitionRankingGroupToGetCompetitionRankingGroupViewModel(ICompetitionRankingGroup group)
        {
            return new CompetitionRankingGroupViewModel(group.Identifier, group.Name,
                group.Competitions.Select(s => CompetitionMapper.CompetitionToSimpleCompetitionViewModel(s)).ToList(),
                group.Rankings.Select(r => RankingMapper.CompetitionRankingToGetCompetitionRankingViewModel((ICompetitionRanking)r)).ToList());
        }
    }
}
