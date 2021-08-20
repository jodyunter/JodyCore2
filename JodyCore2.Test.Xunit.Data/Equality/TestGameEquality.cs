using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Equality
{
    public class TestGameEquality
    {
        public static IEnumerable<object[]> GetDataForGameTest()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();
            var guid4 = Guid.NewGuid();

            var gameGuid1 = Guid.NewGuid();
            var gameGuid2 = Guid.NewGuid();            

            yield return new object[]
            {
                "Equal Scenario",
                new GameDto(gameGuid1, 1, 2, 
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                true
            };
            yield return new object[]
            {
                "Different Game Guids",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid2, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different HomeTeam Guid",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid3, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different away team Guid",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid3, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };       
            yield return new object[]
            {
                "Different Year",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 5, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Day",
                new GameDto(gameGuid1, 1, 10,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Home Score",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                30,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Away Score",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,40, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {        
                "Different Complete",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, true, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, false, true, true),
                false
            };
            yield return new object[]
            {
                "Different Processed",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, true, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Can Tie",
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, true),
                new GameDto(gameGuid1, 1, 2,
                new TeamDto(guid1, "My Name", 5),
                new TeamDto(guid2, "My Name", 5),
                3,4, true, false, false),
                false
            };
        }
        [Theory]
        [MemberData(nameof(GetDataForGameTest))]        
        public void TestGameDtoEquality(string testDescription, GameDto game1, GameDto game2, bool expected)
        {

            var result = game1.Equals(game2);

            Assert.StrictEqual(expected, result);
        }
    }
}
