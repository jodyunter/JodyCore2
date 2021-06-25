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

        public override IBaseRepository<GameDto> SetupRepository()
        {
            gameRepository =  new GameRepository();
            return gameRepository;
        }

        [Test]
        public void ShouldCreateGame()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldUpdateGame()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldGetAll()
        {
            Assert.Fail();
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
