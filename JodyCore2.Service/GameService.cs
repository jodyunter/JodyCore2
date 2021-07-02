using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.Util;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class GameService : IGameService
    {
        private readonly ITeamRepository teamRepository;
        private readonly IGameRepository gameRepository;

        public GameService(ITeamRepository _teamRepository, IGameRepository _gameRepository)
        {
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
        }

        //need to do checks like, if teams play on day
        public IGameSummaryViewModel Create(int year, int day, Guid homeId, Guid awayid)
        {
            using (var context = new JodyContext())
            {
                var homeTeam = teamRepository.GetByIdentifier(homeId, context).FirstOrDefault();
                var awayTeam = teamRepository.GetByIdentifier(awayid, context).FirstOrDefault();
                                
                if (homeTeam == null || awayTeam == null)
                {
                    throw new ApplicationException(string.Format("Home or Away Team Does not exist. Home identifier is {0}.  Away Identifier is {1}", homeId, awayid));
                }
                
                var game = new GameDto(Guid.NewGuid(), year, day, homeTeam, awayTeam, 0, 0, false, false, true);
                gameRepository.Create(game, context);

                context.SaveChanges();

                return GameMapper.GameToGameSummaryViewModel(game);
            }
                
        }

        public IList<IGameSummaryViewModel> GetGames(int year, int firstDay, int lastDay)
        {
            using (var context = new JodyContext())
            {
                return gameRepository.GetByYearAndDayRange(year, firstDay, lastDay, context).Select(g => GameMapper.GameToGameSummaryViewModel(g)).ToList();
            }
        }

        public IList<IGameSummaryViewModel> PlayGamesOnDay(int year, int day)
        {
            using (var context = new JodyContext())
            {
                var games = gameRepository.GetByYearAndDayRangeAndCompleteStatus(year, day, day, false, context).ToList();

                var random = RandomUtility.GetRandom();
                    
                games.ForEach(g =>
                {
                    g.Play(random);
                });

                context.SaveChanges();

                return games.Select(g => GameMapper.GameToGameSummaryViewModel(g)).ToList();
            }
        }

        public IGameSummaryViewModel Play(Guid gameId)
        {
            using (var context = new JodyContext())
            {
                var game = gameRepository.GetByIdentifier(gameId, context).FirstOrDefault();

                if (game == null)
                {
                    throw new ApplicationException(string.Format("Game does not exist. Identifier {0}", gameId));
                }

                if (game.Complete)
                {
                    throw new ApplicationException(string.Format("Game is already complete."));
                }

                var random = RandomUtility.GetRandom();

                game.Play(random);

                context.SaveChanges();

                return GameMapper.GameToGameSummaryViewModel(game);
            }
        }
    }
}
