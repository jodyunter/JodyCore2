using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Rankings;
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
        public IRankingGroupViewModel RankingGroupToRankingGroupViewModel(IRankingGroup group)
        {
            return new RankingGroupViewModel(group.Identifier, group.Name,
                group.Rankings.Select(r => RankingMapper.RankingToRankingViewModel(r)).ToList());
        }
        public ICompetitiongRankingGroupViewModel CompetitionRankingGroupToCompetitionRankingGroupViewModel(ICompetitionRankingGroup group)
        {
            return new CompetitionRankingGroupViewModel(group.Identifier, group.Name,
                group.Competitions.Select(s => CompetitionMapper.CompetitionToSimpleCompetitionViewModel(s)).ToList(),
                group.Rankings.Select(r => RankingMapper.CompetitionRankingToCompetitionRankingViewModel((ICompetitionRanking)r)).ToList());
        }
    }
}
