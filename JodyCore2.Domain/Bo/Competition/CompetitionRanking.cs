using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competition
{
    public class CompetitionRanking:Ranking, ICompetitionRanking
    {
        public CompetitionRanking():base() { }
        public CompetitionRanking(Guid identifier, ITeam team, int rank, ICompetitionRankingGroup group):base(identifier, team, rank, group)
        {            
        }
    }
}
