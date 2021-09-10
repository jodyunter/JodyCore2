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
        

            yield return new object[]
            {
                "Equal Scenario",
                true
            };
            //need a null, not null scenario, and a not null in both
        }
        [Theory]
        [MemberData(nameof(GetDataForStandingsRecordTest))]        
        public void TestGameEquality(string testDescription, StandingsRecord rec1, StandingsRecord rec2, bool expected)
        {

            var result = rec1.Equals(rec2);

            Assert.StrictEqual(expected, result);
        }
    }
}
