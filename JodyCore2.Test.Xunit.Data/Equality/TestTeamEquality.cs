using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Equality
{
    public class TestTeamEquality
    {
        public static IEnumerable<object[]> GetDataForTeamTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            yield return new object[]
            {
                new Team(guid1, "My Name", 5),
                new Team(guid1, "My Name", 5),
                true
            };
            yield return new object[]
            {
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                false
            };
            yield return new object[]
            {
                new Team(guid1, "My Name", 5),
                new Team(guid1, "My Name2", 5),
                false
            };
            yield return new object[]
            {
                new Team(guid1, "My Name", 6),
                new Team(guid1, "My Name", 5),
                false
            };
        }
        [Theory]
        [MemberData(nameof(GetDataForTeamTest))]        
        public void TeamEqualityTest(Team team1, Team team2, bool expected)
        {

            var result = team1.Equals(team2);

            Assert.StrictEqual(expected, result);
        }
    }
}
