using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Equality
{
    public class StandingsEqualityTests
    {
        public static IEnumerable<object[]> GetDataForStandingsTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
        

            yield return new object[]
            {
                "Equal Scenario",
                new Standings(guid1, "My Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                new Standings(guid1, "My Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                true
            };
            yield return new object[]
            {
                "Guid",
                new Standings(guid1, "My Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                new Standings(guid2, "My Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                false
            };
            yield return new object[]
            {
                "Name",
                new Standings(guid1, "My2 Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                new Standings(guid1, "My Name", 1, 2, 1, "No Descr", "No Div", null, false, false, false, false),
                false
            };           

        }
        [Theory]
        [MemberData(nameof(GetDataForStandingsTest))]        
        public void StandingsEqualityTest(string testDescription, Standings rec1, Standings rec2, bool expected)
        {

            var result = rec1.Equals(rec2);

            Assert.StrictEqual(expected, result);
        }
    }
}
