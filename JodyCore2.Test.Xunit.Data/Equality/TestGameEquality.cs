using JodyCore2.Domain.Bo;
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
                new Game(gameGuid1, 1, 2, 
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                true
            };
            yield return new object[]
            {
                "Different Game Guids",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid2, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different HomeTeam Guid",
                new Game(gameGuid1, 1, 2,
                new Team(guid3, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different away team Guid",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid3, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };       
            yield return new object[]
            {
                "Different Year",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 5, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Day",
                new Game(gameGuid1, 1, 10,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Home Score",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                30,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Away Score",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,40, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {        
                "Different Complete",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, true, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, false, true, true),
                false
            };
            yield return new object[]
            {
                "Different Processed",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, true, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                false
            };
            yield return new object[]
            {
                "Different Can Tie",
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, true),
                new Game(gameGuid1, 1, 2,
                new Team(guid1, "My Name", 5),
                new Team(guid2, "My Name", 5),
                3,4, true, false, false),
                false
            };
        }
        [Theory]
        [MemberData(nameof(GetDataForGameTest))]        
        public void TestGameEqualityTest(string testDescription, Game game1, Game game2, bool expected)
        {

            var result = game1.Equals(game2);

            Assert.StrictEqual(expected, result);
        }
    }
}
