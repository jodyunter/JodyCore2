using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Rankings
{
    public class Ranking : IRanking
    {
        public Guid Identifier { get; set;}

        public virtual ITeam Team { get; set;}

        public int Rank { get; set;}

        public virtual IRankingGroup Group { get; set;}

        public Ranking() { }
        public Ranking(Guid identifier, ITeam team, int rank, IRankingGroup group)
        {
            Identifier = identifier;
            Team = team;
            Rank = rank;
            Group = group;
        }

        public void SetRank(int rank)
        {
            Rank = rank;
        }
    }
}
