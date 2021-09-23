using JodyCore2.Data;
using JodyCore2.Data.Repositories.Competitions;
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
        private readonly ICompetitionRepository competitionRepository;

        public RankingService(IRankingGroupRepository rankingGroupRepo,ICompetitionRepository competitionRepo, ITeamRepository teamRepo)
        {
            teamRepository = teamRepo;
            rankingGroupRepository = rankingGroupRepo;
            competitionRepository = competitionRepo;
        }

        public ICompetitiongRankingGroupViewModel CreateCompetitionRakingGroupFromRankingGroup(Guid rankingGroupIdentifier, IList<Guid> competitionIdentifierList)
        {
            using (var context = new JodyContext()) 
            {
                var rankingGroup = rankingGroupRepository.GetByIdentifier(rankingGroupIdentifier, context).First();
                var competitions = competitionRepository.GetCompetitionsByList(competitionIdentifierList, context);
                var newCompetitionGroup = new CompetitionRankingGroup(competitions.ToList<ICompetition>(), rankingGroup.Name, new List<ICompetitionRanking>());

                rankingGroup.Rankings.ToList().ForEach(rankings =>
                {
                    newCompetitionGroup.AddRank(rankings.Team, 1);
                });

                context.Add(newCompetitionGroup);
                context.SaveChanges();

                return RankingGroupMapper.CompetitionRankingGroupToCompetitionRankingGroupViewModel(newCompetitionGroup);
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
