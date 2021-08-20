using JodyCore2.Data.Dto;
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
        public static IEnumerable<object[]> GetDataForTeamDtoTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            yield return new object[]
            {
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid1, "My Name", 5),
                true
            };
            yield return new object[]
            {
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                false
            };
            yield return new object[]
            {
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid1, "My Name2", 5),
                false
            };
            yield return new object[]
            {
                new TeamDto(guid1, "My Name", 6),
                new TeamDto(guid1, "My Name", 5),
                false
            };
        }
        [Theory]
        [MemberData(nameof(GetDataForTeamDtoTest))]        
        public void TestTeamDtoEquality(TeamDto team1, TeamDto team2, bool expected)
        {

            var result = team1.Equals(team2);

            Assert.StrictEqual(expected, result);
        }
    }
}
