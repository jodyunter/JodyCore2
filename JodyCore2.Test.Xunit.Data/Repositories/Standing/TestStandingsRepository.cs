using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Repositories.Standing
{
    public class TestStandingsRepository : TestBaseRepository<Standings>
    {
        public override Standings SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<Standings> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<Standings> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<Standings> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override Standings SetupUpdateData(Standings originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
