using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Standing;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;

namespace JodyCore2.Test.Data
{
    public class TestStandingsRecordRepository : TestBaseRepository<StandingsRecord>
    {
        public override StandingsRecord SetupCreateData(JodyContext context)
        {
            var standings = new Standings(Guid.NewGuid(), "Standings Name", 1, 15, 200, 250, "No Description", "No Division", null);
            var team = new Team(Guid.NewGuid(), "My Team", 5);            

            return new StandingsRecord(Guid.NewGuid(),
                standings,
                team, "My Name", 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        public override IList<StandingsRecord> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }

        public override IList<StandingsRecord> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<StandingsRecord> SetupRepository()
        {
            return new StandingsRecordRepository();
        }

        public override StandingsRecord SetupUpdateData(StandingsRecord originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
