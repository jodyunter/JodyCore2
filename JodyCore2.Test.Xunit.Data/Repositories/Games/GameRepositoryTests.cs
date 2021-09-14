using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using JodyCore2.Test.Xunit.Data.Repositories.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Repositories.Games
{
    public class GameRepositoryTests:BaseRepositoryTests<Game>
    {
        IGameRepository gameRepository;
        ITeamRepository teamRepository;

        public override Game SetupCreateData(JodyContext context)
        {
            var home = new Team(Guid.NewGuid(), "Team 1", 5);
            var away = new Team(Guid.NewGuid(), "Team 2", 5);

            return new Game(Guid.NewGuid(), 25, 5, home, away, 25, 36, true, false, true);
        }

        public override Game SetupUpdateData(Game originalData, JodyContext context)
        {
            var team3 = new Team(Guid.NewGuid(), "Team 3", 5);
            var team4 = new Team(Guid.NewGuid(), "team 4", 5);

            context.Add(team3);
            context.Add(team4);

            context.SaveChanges();

            var updatedData = gameRepository.GetByIdentifier(originalData.Identifier, context).FirstOrDefault();

            updatedData.Home = team3;
            updatedData.Away = team4;

            return updatedData;
        }

        public override IList<Game> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }
        public override IList<Game> SetupGetAllData(JodyContext context)
        {
            var teams = TestTeamRepository.SetupGenericTeams(20, context, teamRepository);
            var list = new List<Game>();

            for (int i = 0; i < 10; i++)
            {
                var gameDto = new Game(Guid.NewGuid(), 15, 1, teams[i], teams[i + 10], 0, 0, false, false, true);
                gameRepository.Create(gameDto, context);

                list.Add(gameDto);
            }

            return list;
        }

        public override IBaseRepository<Game> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository =  new GameRepository();
            return gameRepository;
        }


        protected void SetupGameData(JodyContext context)
        {
            var gameDtos = new List<Game>()
            {
                new Game(Guid.NewGuid(), 1, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, true, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, true, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new Game(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new Game(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new Game(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new Game(Guid.NewGuid(), 2, 2, null, null, 0, 0, false, false, true),
            };

            gameRepository.Create(gameDtos, context);
        }

        [Fact]
        public void ShouldGetByYearAndDateRangeNullLastDay()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {                
                Assert.StrictEqual(6, gameRepository.GetByYearAndDayRange(1, 1, null, context).Count());
                Assert.StrictEqual(10, gameRepository.GetByYearAndDayRange(2, 1, null, context).Count());
            }
        }

        [Fact]
        public void ShouldGetByYearAndDateRangeNotNullLastDay()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                Assert.StrictEqual(1, gameRepository.GetByYearAndDayRange(1, 1, 1, context).Count());
                Assert.StrictEqual(3, gameRepository.GetByYearAndDayRange(1, 1, 2, context).Count());
                Assert.StrictEqual(6, gameRepository.GetByYearAndDayRange(1, 1, 3, context).Count());
                Assert.StrictEqual(6, gameRepository.GetByYearAndDayRange(2, 1, 1, context).Count());
                Assert.StrictEqual(10, gameRepository.GetByYearAndDayRange(2, 1, 2, context).Count());
                Assert.StrictEqual(4, gameRepository.GetByYearAndDayRange(2, 2, 2, context).Count());
            }
        }

        [Fact]
        public void GetByYearAndDayRangeAndCompleteStatusTrue()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                Assert.StrictEqual(0, gameRepository.GetByYearAndDayRangeAndCompleteStatus(1, 1, 5, true, context).Count());                
                Assert.StrictEqual(3, gameRepository.GetByYearAndDayRangeAndCompleteStatus(2, 2, 5, true, context).Count());                
            }
        }

        //todo: need to figure out how to verify the data returned per game is correct
        [Fact]
        public void GetByYearAndDayRangeAndCompleteStatusFalse()
        {
            using (var context = new JodyContext())
            {
                SetupGameData(context);
                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {                
                Assert.StrictEqual(6, gameRepository.GetByYearAndDayRangeAndCompleteStatus(1, 1, 5, false, context).Count());                
                Assert.StrictEqual(1, gameRepository.GetByYearAndDayRangeAndCompleteStatus(2, 2, 2, false, context).Count());
            }
        }

    }
}
