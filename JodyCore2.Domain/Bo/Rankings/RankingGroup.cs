using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Rankings
{
    public class RankingGroup : IRankingGroup, IBO
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
            return Rankings.Where(r => r.Team.Identifier == team.Identifier).FirstOrDefault();
        }

        public void SetRank(ITeam team, int rank)
        {
            if (Rankings == null) Rankings = new List<IRanking>();

            var currentRank = Rankings.Where(r => r.Team.Identifier == team.Identifier).FirstOrDefault();
                
            if (currentRank == null)
            {
                AddRank(team, rank);
            }
            else
            {
                currentRank.SetRank(rank);
            }
                
        }        

        public virtual IRanking GetByOrder(int rank)
        {
            if (Rankings.Count() < rank)
            {
                return null;
            }

            return Rankings.OrderBy(t => t.Rank).ToArray()[rank - 1];
        }

        public virtual void AddRank(ITeam team, int rank)
        {
            if (Rankings == null) Rankings = new List<IRanking>();

            Rankings.Add(new Ranking(Guid.NewGuid(), team, rank, this));
        }

        public IRanking GetByRank(int rank)
        {
            return Rankings.Where(r => r.Rank == rank).FirstOrDefault();
        }

        public IRanking GetTeamByRank(int rank)
        {
            return Rankings.Where(r => r.Rank == rank).FirstOrDefault();
        }
    }
}
