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
    public class TestRankingRepository : TestBaseRepository<Ranking>
    {
        public override Ranking SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<Ranking> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<Ranking> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<Ranking> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override Ranking SetupUpdateData(Ranking originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
