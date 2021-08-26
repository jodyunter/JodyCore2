using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Rankings
{
    public class RankingGroup : IRankingGroup
    {
        public Guid Identifier { get; set; }

        public string Name { get; set; }

        public virtual IList<IRanking> Rankings { get; set; }

        public RankingGroup() { }

        public RankingGroup(Guid identifier, string name, IList<IRanking> rankings)
        {
            Identifier = identifier;
            Name = name;
            Rankings = rankings;
        }

        public IRanking GetRankingByTeam(ITeam team)
        {
            return Rankings.Where(r => r.Team.Identifier == team.Identifier).First();
        }

        public void SetRank(ITeam team, int rank)
        {
            Rankings.Where(r => r.Team.Identifier == team.Identifier).First().SetRank(rank);
        }
    }
}
