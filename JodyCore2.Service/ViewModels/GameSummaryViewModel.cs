using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class GameSummaryViewModel : IGameSummaryViewModel
    {
        public Guid Identifier { get; set; }

        public int Day { get; set; }

        public int Year { get; set; }

        public Guid HomeTeamIdentifier { get; set; }

        public string HomeTeamName { get; set; }

        public Guid AwayTeamIdentifier { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public bool Complete { get; set; }

        public GameSummaryViewModel(Guid identifier, int day, int year, Guid homeTeamIdentifier, string homeTeamName, Guid awayTeamIdentifier, string awayTeamName, int homeScore, int awayScore, bool complete)
        {
            Identifier = identifier;
            Day = day;
            Year = year;
            HomeTeamIdentifier = homeTeamIdentifier;
            HomeTeamName = homeTeamName;
            AwayTeamIdentifier = awayTeamIdentifier;
            AwayTeamName = awayTeamName;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Complete = complete;
        }
    }
}
