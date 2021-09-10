using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Standing
{
    public class TestStandingsRecordRepository : TestBaseRepository<StandingsRecord>
    {
        public override StandingsRecord SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<StandingsRecord> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<StandingsRecord> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<StandingsRecord> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override StandingsRecord SetupUpdateData(StandingsRecord originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
