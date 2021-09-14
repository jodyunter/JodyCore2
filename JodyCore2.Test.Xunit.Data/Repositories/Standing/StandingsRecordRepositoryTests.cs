using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Standing;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Standing
{
    public class StandingsRecordRepositoryTests : BaseRepositoryTests<StandingsRecord>
    {
        public override StandingsRecord SetupCreateData(JodyContext context)
        {
            var standings = Utility.CreateStandingsNoRecords();            

            var team = new Team(Guid.NewGuid(), "My Name", 5);            

            var record = new StandingsRecord(Guid.NewGuid(), standings, team, "My Name", 5, 4, 3, 2, 1, 0, 6, 7, 8);            
            standings.Records.Add(record);            

            return record;
        }

        public override IList<StandingsRecord> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }

        public override IList<StandingsRecord> SetupGetAllData(JodyContext context)
        {
            var standings = Utility.CreateStandingsNoRecords();
            var list = new List<StandingsRecord>();

            for (int i = 0; i < 10; i++)
            {
                var team = new Team(Guid.NewGuid(), "Team " + i, 5);

                var record = new StandingsRecord(Guid.NewGuid(), standings, team, "My Name", 5, 4, 3, 2, 1, 0, 6, 7, 8);
                standings.Records.Add(record);
                list.Add(record);

                context.AddRange(list);
            }

            return list;
        }

        public override IBaseRepository<StandingsRecord> SetupRepository()
        {
            return new StandingsRecordRepository();
        }

        public override StandingsRecord SetupUpdateData(StandingsRecord originalData, JodyContext context)
        {
            var standings = Utility.CreateStandingsNoRecords();
            var newTeam = new Team(Guid.NewGuid(), "Team New", 5);
            context.Add(newTeam);
            context.Add(standings);

            context.SaveChanges();

            var updatedData = Repository.GetByIdentifier(originalData.Identifier, context).FirstOrDefault();

            updatedData.Team = newTeam;
            updatedData.ParentStandings = standings;

            updatedData.OverTimeWins = 100;
            updatedData.GoalsFor = 25;

            return updatedData;
        }
    }
}
