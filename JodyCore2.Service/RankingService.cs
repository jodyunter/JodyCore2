using JodyCore2.Data;
using JodyCore2.Data.Repositories.Rankings;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Rankings;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class RankingService : IRankingService
    {
        private readonly ITeamRepository teamRepository;
        private readonly IRankingGroupRepository rankingGroupRepository;
        private readonly ICompetitionRepoistory competitionRepoistory;

        public RankingService(IRankingGroupRepository rankingRep,ICompetitionRepository competitionRepo, ITeamRepository teamRepo)
        {
            teamRepository = teamRepo;
        }

        public ICompetitiongRankingGroupViewModel CreateCompetitionRakingGroupFromRankingGroup(Guid rankingGroupIdentifier, IList<Guid> competitionIdentifierList)
        {
            using (var context = new JodyContext()) 
            {
                var rankingGroup = rankingGroupRepository.GetByIdentifier(rankingGroupIdentifier, context).First();
                var newCompetitionGroup = new CompetitionRankingGroup(new List<ICompetition>(), )
            }
        }

        public IRankingViewModel CreateRanking(IRankingGroupViewModel group, ITeamViewModel team, int initialRank)
        {
            throw new NotImplementedException();
        }

        public IRankingGroupViewModel CreateRankingGroup(string name, IList<ITeamViewModel> teamViews, int defaultRank = 1)
        {
            var newGroup = new RankingGroup(name, new List<IRanking>());

            using (var context = new JodyContext()) {
                teamViews.ToList().ForEach(teamView =>
                {
                    var team = teamRepository.GetByIdentifier(teamView.Identifier, context).First();

                    newGroup.AddRank(team, defaultRank);
                });

                context.Add(newGroup);
                context.SaveChanges();
            }

            return RankingGroupMapper.RankingGroupToRankingGroupViewModel(newGroup);
        }
    }
}
