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
        public IList<ICompetition> Competitions { get; set; }

        public CompetitionRankingGroup() { }

        public CompetitionRankingGroup(IList<ICompetition> competitions, string name, IList<ICompetitionRanking> rankings) 
            :this(Guid.NewGuid(), competitions, name, rankings)
        {            
        }

        public CompetitionRankingGroup(Guid identifier, IList<ICompetition> competitions, string name, IList<ICompetitionRanking> rankings) : base(identifier, name, rankings != null? rankings.ToList<IRanking>():null)
        {
            Competitions = competitions;
        }
    }
}
