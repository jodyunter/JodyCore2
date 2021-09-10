using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Equality
{
    public class TestStandingsRecordEquality
    {
        public static IEnumerable<object[]> GetDataForStandingsRecordTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var teamGuid1 = Guid.NewGuid();
            var teamGuid2 = Guid.NewGuid();

            var standingsGuid1 = Guid.NewGuid();
            var standingsGuid2 = Guid.NewGuid();

            yield return new object[]
            {
                "Equal Scenario",
                new StandingsRecord(guid1, 
                    new Standings() { Identifier = standingsGuid1 }, 
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7 ,8 , 9),
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid1 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7, 8, 9),
                true,
            };
            yield return new object[]
            {
                "Different guid",
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid1 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7 ,8 , 9),
                new StandingsRecord(guid2,
                    new Standings() { Identifier = standingsGuid1 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7, 8, 9),
                false,
                };
            yield return new object[]
            {
                "Different standings",
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid1 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7 ,8 , 9),
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid2 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7, 8, 9),
                false,
                };
            yield return new object[]
{
                "Different teams",
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid2 },
                    new Team(teamGuid2, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7 ,8 , 9),
                new StandingsRecord(guid1,
                    new Standings() { Identifier = standingsGuid2 },
                    new Team(teamGuid1, "Yes", 5),
                    "Name?",
                    1, 2, 3, 4, 5, 6, 7, 8, 9),
                false,
    };
            //need a null, not null scenario, and a not null in both
        }
        [Theory]
        [MemberData(nameof(GetDataForStandingsRecordTest))]        
        public void StandingsRecordequalityTest(string testDescription, StandingsRecord rec1, StandingsRecord rec2, bool expected)
        {

            var result = rec1.Equals(rec2);

            Assert.StrictEqual(expected, result);
        }
    }
}
