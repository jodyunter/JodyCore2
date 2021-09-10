using JodyCore2.Data;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Standing;
using JodyCore2.Data.Repositories.Teams;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Util;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JodyCore2.Service
{
    //todo, we'll want a competition ID for using this
    //todo, we should probably use a web api for getting the teams or verifying they exist
    public class GameService : IGameService
    {
        private readonly ITeamRepository teamRepository;
        private readonly IGameRepository gameRepository;
        private readonly IStandingsRepository standingsRepository;

        public GameService(ITeamRepository _teamRepository, IGameRepository _gameRepository, IStandingsRepository _standingsRepository)
        {
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
            standingsRepository = _standingsRepository;
        }

        //need to do checks like, if teams play on day        
        //todo need to allow mulitple new game creation from schedule games
        public IGameSummaryViewModel Create(int year, int day, Guid homeId, Guid awayId)
        {
            using (var context = new JodyContext())
            {
                var homeTeam = teamRepository.GetByIdentifier(homeId, context).FirstOrDefault();
                var awayTeam = teamRepository.GetByIdentifier(awayId, context).FirstOrDefault();
                                
                if (homeTeam == null || awayTeam == null)
                {
                    throw new ApplicationException(string.Format("Home or Away Team Does not exist. Home identifier is {0}.  Away Identifier is {1}", homeId, awayId));
                }
                
                var game = new Game(Guid.NewGuid(), year, day, homeTeam, awayTeam, 0, 0, false, false, true);
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
