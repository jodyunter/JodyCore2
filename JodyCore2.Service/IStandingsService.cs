using JodyCore2.Domain;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public interface IStandingsService
    {
        IStandingsViewModel Create(string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<ITeamViewModel> teamsToInclude);
        IStandingsViewModel GetByIdentifier(Guid guid);
        IStandingsViewModel ProcessGames(Guid standingsIdentifier);
        IStandingsViewModel Sort(Guid guid);
        IGameSummaryViewModel CreateStandingsGame(Guid standingsIdentifier, int year, int day, Guid homeId, Guid awayId);
        IList<IGameSummaryViewModel> GetStandingsGames(Guid standingsIdentifier, int year, int firstDay, int lastDay);
    }
}
