using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Competitions
{
    public class CompetitionGameRepositoryTests : BaseRepositoryTests<CompetitionGame>
    {
        public override CompetitionGame SetupCreateData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<CompetitionGame> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<CompetitionGame> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<CompetitionGame> SetupRepository()
        {
            throw new NotImplementedException();
        }

        public override CompetitionGame SetupUpdateData(CompetitionGame originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }

}
