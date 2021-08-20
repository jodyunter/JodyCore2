using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Data
{
    public class TestStandingsRecordRepository : TestBaseRepository<StandingsRecordDto>
    {
        public override StandingsRecordDto SetupCreateData(JodyContext context)
        {
            var standings = new StandingsDto(Guid.NewGuid(), "Standings Name", 1, 15, 200, 250, "No Description", "No Division", null);
            var team = new TeamDto(Guid.NewGuid(), "My Team", 5);            

            return new StandingsRecordDto(Guid.NewGuid(),
                standings,
                team, 1, "Top Division", "My Name", 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        public override IList<StandingsRecordDto> SetupDeleteData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<StandingsRecordDto> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<StandingsRecordDto> SetupRepository()
        {
            return new StandingsRecordRepository();
        }

        public override StandingsRecordDto SetupUpdateData(StandingsRecordDto originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }
    }
}
