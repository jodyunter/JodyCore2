using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Data
{
    public class TestGameRepository:TestBaseRepository<GameDto>
    {
        IGameRepository gameRepository;
        ITeamRepository teamRepository;

        public override GameDto SetupCreateData(JodyContext context)
        {
            var home = new TeamDto(Guid.NewGuid(), "Team 1", 5);
            var away = new TeamDto(Guid.NewGuid(), "Team 2", 5);

            return new GameDto(Guid.NewGuid(), 5, 25, home, away, 25, 36, true, false, true);
        }

        public override GameDto SetupUpdateData(GameDto originalData, JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IList<GameDto> SetupGetAllData(JodyContext context)
        {
            throw new NotImplementedException();
        }

        public override IBaseRepository<GameDto> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository =  new GameRepository();
            return gameRepository;
        }


        [Test]
        public void ShouldGetByYearAndDateRange()
        {
            Assert.Fail();
        }

        [Test]
        public void GetByYearAndDayRangeAndCompleteStatus()
        {
            Assert.Fail();
        }


    }
}
