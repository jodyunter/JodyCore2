using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class PlayoffGameSummaryViewModel : GameSummaryViewModel, IPlayoffGameSummaryViewModel
    {
        public string Series { get; set; }


        public PlayoffGameSummaryViewModel(Guid identifier, string seriesName, int day, int year, Guid homeTeamIdentifier, string homeTeamName, Guid awayTeamIdentifier, string awayTeamName, int homeScore, int awayScore, bool complete)
            :base(identifier, day, year, homeTeamIdentifier, homeTeamName, awayTeamIdentifier, awayTeamName, homeScore, awayScore, complete)
        {
            Series = seriesName;
        }
    }
}
