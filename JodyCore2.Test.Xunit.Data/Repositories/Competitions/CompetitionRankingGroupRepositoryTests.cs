using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Competitions
{
    public class CompetitionRankingGroupRepositoryTests : BaseRepositoryTests<CompetitionRankingGroup>
    {
        public override CompetitionRankingGroup SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<CompetitionRankingGroup> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<CompetitionRankingGroup> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<CompetitionRankingGroup> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override CompetitionRankingGroup SetupUpdateData(CompetitionRankingGroup originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
