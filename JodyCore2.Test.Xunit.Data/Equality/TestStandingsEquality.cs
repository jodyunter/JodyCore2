using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Equality
{
    public class TestStandingsEquality
    {
        public static IEnumerable<object[]> GetDataForStandingsTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
        

            yield return new object[]
            {
                "Equal Scenario",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                true
            };
            yield return new object[]
            {
                "Guid",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid2, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "Name",
                new Standings(guid1, "My2 Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "StartYear",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 10, 2, 3, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "EndYear",
                new Standings(guid1, "My Name", 1, 20, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "StartDay",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 30, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "EndDay",
                new Standings(guid1, "My Name", 1, 2, 3, 40, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "Description",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "ANo Descr", "No Div", null),
                false
            };
            yield return new object[]
            {
                "Division",
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "BNo Div", null),
                new Standings(guid1, "My Name", 1, 2, 3, 4, "No Descr", "No Div", null),
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
