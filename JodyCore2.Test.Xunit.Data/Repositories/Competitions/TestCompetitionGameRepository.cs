﻿using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Competitions;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data.Repositories.Competitions
{
    public class TestCompetitionGameRepository : TestBaseRepository<CompetitionGame>
    {
        ICompetitionGameRepository gameRepository;
        ITeamRepository teamRepository;

        public override CompetitionGame SetupCreateData(JodyContext context)
        {
            var 
            var home = new Team(Guid.NewGuid(), "Team 1", 5);
            var away = new Team(Guid.NewGuid(), "Team 2", 5);

            return new CompetitionGame(Guid.NewGuid(), 25, 5, home, away, 25, 36, true, false, true);
        }

        public override CompetitionGame SetupUpdateData(CompetitionGame originalData, JodyContext context)
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

        public override IList<CompetitionGame> SetupDeleteData(JodyContext context)
        {
            return SetupGetAllData(context);
        }
        public override IList<CompetitionGame> SetupGetAllData(JodyContext context)
        {
            var teams = TestTeamRepository.SetupGenericTeams(20, context, teamRepository);
            var list = new List<CompetitionGame>();

            for (int i = 0; i < 10; i++)
            {
                var gameDto = new CompetitionGame(Guid.NewGuid(), 15, 1, teams[i], teams[i + 10], 0, 0, false, false, true);
                gameRepository.Create(gameDto, context);

                list.Add(gameDto);
            }

            return list;
        }

        public override IBaseRepository<CompetitionGame> SetupRepository()
        {
            teamRepository = new TeamRepository();
            gameRepository = new CompetitionGameRepository();
            return gameRepository;
        }


        protected void SetupCompetitionGameData(JodyContext context)
        {
            var gameDtos = new List<CompetitionGame>()
            {
                new CompetitionGame(Guid.NewGuid(), 1, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 3, 1, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, true, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, true, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 1, 2, null, null, 0, 0, false, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 2, null, null, 0, 0, true, false, true),
                new CompetitionGame(Guid.NewGuid(), 2, 2, null, null, 0, 0, false, false, true),
            };

            gameRepository.Create(gameDtos, context);
        }

        [Fact]
        public void ShouldGetByYearAndDateRangeNullLastDay()
        {
            using (var context = new JodyContext())
            {
                SetupCompetitionGameData(context);
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
                SetupCompetitionGameData(context);
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
                SetupCompetitionGameData(context);
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
                SetupCompetitionGameData(context);
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
}
