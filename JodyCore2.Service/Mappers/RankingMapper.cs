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
    public static class RankingMapper
    {
        public static ICompetitionRankingViewModel CompetitionRankingToCompetitionRankingViewModel(ICompetitionRanking data)
        {
            return new CompetitionRankingViewModel(data.Identifier, data.Group.Identifier, data.Group.Name, data.Team.Identifier, data.Team.Name, data.Rank);
        }

        public static IRankingViewModel RankingToRankingViewModel(IRanking data)
        {
            return new RankingViewModel(data.Identifier, data.Group.Identifier, data.Group.Name, data.Team.Identifier, data.Team.Name, data.Rank);
        }
    }
}
