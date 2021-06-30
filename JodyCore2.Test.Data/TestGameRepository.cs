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

            return new GameDto(Guid.NewGuid(), 25, 5, home, away, 25, 36, true, false, true);
        }

        public override GameDto SetupUpdateData(GameDto originalData, JodyContext context)
        {
            var team3 = new TeamDto(Guid.NewGuid(), "Team 3", 5);
            var team4 = new TeamDto(Guid.NewGuid(), "team 4", 5);

            var updatedData = gameRepository.GetByIdentifier(originalData.Identifier, context).FirstOrDefault();

            updatedData.Home = team3;
            updatedData.Away = team4;

            return updatedData;
        }


        public override IList<GameDto> SetupGetAllData(JodyContext context)
        {
            var teams = TestTeamRepository.SetupGenericTeams(20, context, teamRepository);
            var list = new List<GameDto>();

            for (int i = 0; i < 10; i++)
            {
                var gameDto = new GameDto(Guid.NewGuid(), 15, 1, teams[i], teams[i + 10], 0, 0, false, false, true);
                gameRepository.Create(gameDto, context);
            }

            return list;
        }

        public override IBaseRepository<GameDto> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository =  new GameRepository();
            return gameRepository;
        }


        public void SetupGameData(JodyContext context)
        {
            var gameDtos = new List<GameDto>()
            {
                new GameDto(Guid.NewGuid(), 1, 1, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 1, 3, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 1, 3, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 1, 3, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, true, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, true, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new GameDto(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new GameDto(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new GameDto(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new GameDto(Guid.NewGuid(), 2, 2, null, null, 0, 0, false, false, true),
            };

            gameRepository.Create(gameDtos, context);
        }

        [Test]
        public void ShouldGetByYearAndDateRangeNullLastDay()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {                
                Assert.AreEqual(6, gameRepository.GetByYearAndDayRange(1, 1, null, context).Count());
                Assert.AreEqual(10, gameRepository.GetByYearAndDayRange(2, 1, null, context).Count());
            }
        }

        [Test]
        public void ShouldGetByYearAndDateRangeNotNullLastDay()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                Assert.AreEqual(1, gameRepository.GetByYearAndDayRange(1, 1, 1, context).Count());
                Assert.AreEqual(3, gameRepository.GetByYearAndDayRange(1, 1, 2, context).Count());
                Assert.AreEqual(6, gameRepository.GetByYearAndDayRange(1, 1, 3, context).Count());
                Assert.AreEqual(6, gameRepository.GetByYearAndDayRange(2, 1, 1, context).Count());
                Assert.AreEqual(10, gameRepository.GetByYearAndDayRange(2, 1, 2, context).Count());
                Assert.AreEqual(4, gameRepository.GetByYearAndDayRange(2, 2, 2, context).Count());
            }
        }

        [Test]
        public void GetByYearAndDayRangeAndCompleteStatusTrue()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                Assert.AreEqual(0, gameRepository.GetByYearAndDayRangeAndCompleteStatus(1, 1, 5, true, context).Count());                
                Assert.AreEqual(3, gameRepository.GetByYearAndDayRangeAndCompleteStatus(2, 2, 5, true, context).Count());                
            }
        }

        //todo: need to figure out how to verify the data returned per game is correct
        [Test]
        public void GetByYearAndDayRangeAndCompleteStatusFalse()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {                
                Assert.AreEqual(6, gameRepository.GetByYearAndDayRangeAndCompleteStatus(1, 1, 5, false, context).Count());                
                Assert.AreEqual(1, gameRepository.GetByYearAndDayRangeAndCompleteStatus(2, 2, 2, false, context).Count());
            }
        }

    }
}
