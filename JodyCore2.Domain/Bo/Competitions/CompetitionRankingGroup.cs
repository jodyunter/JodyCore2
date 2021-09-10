using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competitions
{
    public class CompetitionRankingGroup : RankingGroup, ICompetitionRankingGroup
    {
        public ICompetition Competition { get; set; }

        public CompetitionRankingGroup() { }

        public CompetitionRankingGroup(Guid identifier, ICompetition competition, string name, IList<ICompetitionRanking> rankings) : base(identifier, name, rankings.ToList<IRanking>())
        {
            Competition = competition;
        }
    }
}
