using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp.Views
{
    public class GameView
    {        
        public static string GetGameSummaryView(IGameSummaryViewModel model)
        {
            if (model is IPlayoffGameSummaryViewModel)
            {
                var pModel = (IPlayoffGameSummaryViewModel)model;

                string formatter = "R{6}. ({5}) {0}. {1} - {2} : {3} - {4}";

                return string.Format(formatter, model.Day, model.HomeTeamName, model.HomeScore, model.AwayTeamName, model.AwayScore, pModel.Series, pModel.Round);
            }
            else
            {
                string formatter = "{0}. {1} - {2} : {3} - {4}";

                return string.Format(formatter, model.Day, model.HomeTeamName, model.HomeScore, model.AwayTeamName, model.AwayScore);
            }
        }
    }
}
