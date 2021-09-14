using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Rankings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Rankings
{
    public class RankingGroupRepositoryTests : BaseRepositoryTests<RankingGroup>
    {
        public override RankingGroup SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<RankingGroup> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<RankingGroup> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<RankingGroup> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override RankingGroup SetupUpdateData(RankingGroup originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
