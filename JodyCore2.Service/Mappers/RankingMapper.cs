using JodyCore2.Domain.Bo.Competitions;
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
        public static ICompetitionRankingViewModel CompetitionRankingToGetCompetitionRankingViewModel(ICompetitionRanking data)
        {
            return new CompetitionRankingViewModel(data.Identifier, data.Group.Identifier, data.Group.Name, data.Team.Identifier, data.Team.Name, data.Rank);
        }
    }
}
