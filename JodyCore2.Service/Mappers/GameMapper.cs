using JodyCore2.Domain.Bo;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Mappers
{
    public class GameMapper
    {
        public static IGameSummaryViewModel GameToGameSummaryViewModel(IGame game)
        {
            return new GameSummaryViewModel(game.Identifier, game.Day, game.Year,
                game.Home.Identifier, game.Home.Name, game.Away.Identifier, game.Away.Name,
                game.HomeScore, game.AwayScore, game.Complete);
        }
    }
}
