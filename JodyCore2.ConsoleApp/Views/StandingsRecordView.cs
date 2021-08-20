using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp.Views
{
    public class StandingsRecordView
    {
        public static string BASIC_FORMAT = "{0,3}. {1,15} {2,3} {3,3} {4,3} {5,5} {6,5} {7,5} {8,5} {9,5}";

        public static string RecordView(IStandingsRecordViewModel model)
        {
            return string.Format(BASIC_FORMAT,
                model.Rank,
                model.Name,
                model.Wins,
                model.Loses,
                model.Ties,
                model.Points,
                model.GamesPlayed,
                model.GoalsFor,
                model.GoalsAgainst,
                model.GoalDifference);
        }
    }
}
